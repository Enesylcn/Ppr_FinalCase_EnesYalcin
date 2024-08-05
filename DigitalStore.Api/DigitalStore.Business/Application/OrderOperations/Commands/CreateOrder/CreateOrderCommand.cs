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

namespace DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderRequest Request) : IRequest<ApiResponse<OrderResponse>>;

    public class CreateOrderCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<OrderRequest, Order>(request.Request);
            await unitOfWork.OrderRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<OrderResponse>(mapped);
            return new ApiResponse<OrderResponse>(response);
        }
    }
}
