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

namespace DigitalStore.Business.Application.OrderOperations.Queries.GetOrderDetails
{
    public record GetOrderByIdQuery(long OrderId) : IRequest<ApiResponse<OrderResponse>>;

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }


        public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.OrderRepository.GetById(request.OrderId);
            var mapped = mapper.Map<OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(mapped);
        }
    }
}
