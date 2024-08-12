using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CategoryOperations.Commands.DeleteCategory;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;

namespace DigitalStore.Business.Application.CategoryOperations.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(long categoryId, CategoryRequest Request) : IRequest<ApiResponse>;
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CategoryRepository.GetById(request.categoryId);
            if (entity == null)
            {
                return new ApiResponse("No related Category found.");
            }

            mapper.Map(request.Request, entity);
            unitOfWork.CategoryRepository.Update(entity);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
