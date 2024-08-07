using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ProductOperations.Commands.UpdateProduct;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ProductOperations.Queries.GetProduct
{
    public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>
    {
        private readonly IUnitOfWork<Product> unitOfWork;
        private readonly IMapper mapper;
        public GetAllProductQueryHandler(IUnitOfWork<Product> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> entityList = await unitOfWork.GenericRepository.GetAll("Product");
            var mappedList = mapper.Map<List<ProductResponse>>(entityList);
            return new ApiResponse<List<ProductResponse>>(mappedList);
        }
    }
}
