using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Base;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using DigitalStore.Data.Domain;

namespace DigitalStore.Business.Application.ProductOperations.Queries.GetOnsaleProduct
{
    public record GetOnSaleProductQuery(bool IsOnSale) : IRequest<ApiResponse<List<ProductResponse>>>;  

    public class GetOnSaleProductQueryHandler : IRequestHandler<GetOnSaleProductQuery, ApiResponse<List<ProductResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetOnSaleProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetOnSaleProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> entityList = await unitOfWork.ProductRepository.Where(p => p.IsActive == request.IsOnSale, "ProductCategories.Category");
            var mappedList = mapper.Map<List<ProductResponse>>(entityList);
            return new ApiResponse<List<ProductResponse>>(mappedList);
        }
    }
}
