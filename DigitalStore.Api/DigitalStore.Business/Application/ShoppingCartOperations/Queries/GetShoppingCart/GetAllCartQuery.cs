using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCart
{
    public record GetAllCartQuery() : IRequest<ApiResponse<List<ShoppingCartResponse>>>;

    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartQuery, ApiResponse<List<ShoppingCartResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllCartQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ShoppingCartResponse>>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
        {

            List<ShoppingCart> entityList = await unitOfWork.ShoppingCartRepository.GetAll();
            var mappedList = mapper.Map<List<ShoppingCartResponse>>(entityList);
            return new ApiResponse<List<ShoppingCartResponse>>(mappedList);
        }
    }
}
