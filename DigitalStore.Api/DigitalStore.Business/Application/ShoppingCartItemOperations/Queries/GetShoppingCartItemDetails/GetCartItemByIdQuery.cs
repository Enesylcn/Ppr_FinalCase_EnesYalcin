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
using DigitalStore.Data.Domain;

namespace DigitalStore.Business.Application.ShoppingCartItemOperations.Queries.GetShoppingCartItemDetails
{
    public record GetCartItemByIdQuery(long CartItemId) : IRequest<ApiResponse<ShoppingCartItemResponse>>;

    public class GetCartItemByIdQueryHandler : IRequestHandler<GetCartItemByIdQuery, ApiResponse<ShoppingCartItemResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public GetCartItemByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }


        public async Task<ApiResponse<ShoppingCartItemResponse>> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ShoppingCartItemRepository.GetById(request.CartItemId);
            var mapped = mapper.Map<ShoppingCartItemResponse>(entity);
            return new ApiResponse<ShoppingCartItemResponse>(mapped);
        }
    }
}
