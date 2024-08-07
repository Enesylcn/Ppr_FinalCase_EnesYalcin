using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderOperations.Queries.GetOrder;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ProductOperations.Commands.CreateProduct
{
    public record CreateProductCommand(ProductRequest Request) : IRequest<ApiResponse<ProductResponse>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ProductRequest, Product>(request.Request);
            await unitOfWork.ProductRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<ProductResponse>(mapped);
            return new ApiResponse<ProductResponse>(response);
        }
    }
}
