using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.UnitOfWork;
using MediatR;

namespace DigitalStore.Business.Application.CategoryOperations.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(long categoryId) : IRequest<ApiResponse>;
    public class DeleteCategoryCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CategoryRepository.Delete(request.categoryId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
