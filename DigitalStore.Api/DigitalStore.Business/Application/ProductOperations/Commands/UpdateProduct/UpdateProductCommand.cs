using AutoMapper;
using Azure;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;

namespace DigitalStore.Business.Application.ProductOperations.Commands.UpdateProduct
{
    public record UpdateProductCommand(long productId, ProductRequest Request) : IRequest<ApiResponse>;
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ProductRepository.GetById(request.productId);
            if (entity == null)
            {
                return new ApiResponse("No related products found.");
            }

            // Map the request to the existing entity
            mapper.Map(request.Request, entity);
            unitOfWork.ProductRepository.Update(entity);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
