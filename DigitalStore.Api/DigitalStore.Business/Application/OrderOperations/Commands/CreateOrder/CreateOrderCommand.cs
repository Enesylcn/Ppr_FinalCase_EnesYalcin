using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderDetailOperations.Queries.GetOrderDetail;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderRequest Request) : IRequest<ApiResponse<OrderResponse>>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
        }

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<OrderRequest, Order>(request.Request);
            mapped.OrderNumber = new Random().Next(1000000, 9999999).ToString();
            mapped.UserId = sessionContext.Session.UserId;
            mapped.Name = $"{mapped.Id + 1 }.Order";
            await unitOfWork.OrderRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<OrderResponse>(mapped);
            return new ApiResponse<OrderResponse>(response);
        }
    }
}
