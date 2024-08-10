using AutoMapper;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCart;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCartByUserId
{
    public record GetCartByUserIdQuery(string userId) : IRequest<ApiResponse<List<ShoppingCartResponse>>>;
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, ApiResponse<List<ShoppingCartResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetCartByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ShoppingCartResponse>>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await unitOfWork.ShoppingCartRepository
                .Query(sc => sc.UserId == request.userId)
                .Include(sc => sc.ShoppingCartItems)
                .ThenInclude(sci => sci.Product)
                .FirstOrDefaultAsync();
            var mappedList = mapper.Map<List<ShoppingCartResponse>>(shoppingCart);
            return new ApiResponse<List<ShoppingCartResponse>>(mappedList);

        }
    }
}
