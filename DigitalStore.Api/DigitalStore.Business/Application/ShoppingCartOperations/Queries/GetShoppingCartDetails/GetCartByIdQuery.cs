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

namespace DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCartDetails
{
    public record GetCartByIdQuery(long ShoppingCartId) : IRequest<ApiResponse<ShoppingCartResponse>>;

    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, ApiResponse<ShoppingCartResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public GetCartByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }


        public async Task<ApiResponse<ShoppingCartResponse>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ShoppingCartRepository.GetById(request.ShoppingCartId);
            var mapped = mapper.Map<ShoppingCartResponse>(entity);
            return new ApiResponse<ShoppingCartResponse>(mapped);
        }
    }
}
