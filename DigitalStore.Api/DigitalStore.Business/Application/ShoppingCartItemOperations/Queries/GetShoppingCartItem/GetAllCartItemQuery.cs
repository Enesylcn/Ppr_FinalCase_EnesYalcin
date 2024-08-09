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

namespace DigitalStore.Business.Application.ShoppingCartItemOperations.Queries.GetShoppingCartItem
{
    public record GetAllCartItemQuery() : IRequest<ApiResponse<List<ShoppingCartItemResponse>>>;

    public class GetAllCartItemQueryHandler : IRequestHandler<GetAllCartItemQuery, ApiResponse<List<ShoppingCartItemResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllCartItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ShoppingCartItemResponse>>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
        {
            List<ShoppingCartItem> entityList = await unitOfWork.ShoppingCartItemRepository.GetAll();
            var mappedList = mapper.Map<List<ShoppingCartItemResponse>>(entityList);
            return new ApiResponse<List<ShoppingCartItemResponse>>(mappedList);
        }
    }
}
