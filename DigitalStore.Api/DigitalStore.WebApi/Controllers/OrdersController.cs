using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder;
using DigitalStore.Business.Application.OrderOperations.Commands.DeleteOrder;
using DigitalStore.Business.Application.OrderOperations.Commands.UpdateOrder;
using DigitalStore.Business.Application.OrderOperations.Queries.GetOrder;
using DigitalStore.Business.Application.OrderOperations.Queries.GetOrderDetails;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<OrderResponse>>> Get()
        {
            var operation = new GetAllOrderQuery();
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpGet("{orderId}")]
        public async Task<ApiResponse<OrderResponse>> GetByOrderId([FromRoute] long orderId)
        {
            var operation = new GetOrderByIdQuery(orderId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] OrderRequest value)
        {
            var operation = new CreateOrderCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{orderId}")]
        public async Task<ApiResponse> Put(long orderId, [FromBody] OrderRequest value)
        {
            var operation = new UpdateOrderCommand(orderId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{orderId}")]
        public async Task<ApiResponse> Delete(long orderId)
        {
            var operation = new DeleteOrderCommand(orderId);
            var result = await mediator.Send(operation);
            return result;
        }

    }
}
