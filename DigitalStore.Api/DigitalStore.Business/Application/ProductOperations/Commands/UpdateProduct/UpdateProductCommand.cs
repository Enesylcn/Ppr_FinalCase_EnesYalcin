using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ProductOperations.Commands.DeleteProduct;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ProductOperations.Commands.UpdateProduct
{
    public record UpdateProductCommand(long ProductId, ProductRequest Request) : IRequest<ApiResponse>;
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
            var mapped = mapper.Map<ProductRequest, Product>(request.Request);
            mapped.Id = request.ProductId;
            unitOfWork.ProductRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
