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
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        ////[Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<OrderResponse>>> Get()
        {
            var operation = new GetAllOrderQuery();
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpGet("{orderId}")]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse<OrderResponse>> GetByOrderId([FromRoute] long orderId)
        {
            var operation = new GetOrderByIdQuery(orderId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse<OrderResponse>> Post([FromBody] OrderRequest value)
        {
            var operation = new CreateOrderCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{orderId}")]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long orderId, [FromBody] OrderRequest value)
        {
            var operation = new UpdateOrderCommand(orderId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{orderId}")]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long orderId)
        {
            var operation = new DeleteOrderCommand(orderId);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet("ByParameters")]
        ////[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<List<OrderResponse>>> GetByParameters(
        //    [FromQuery] long? OrderNumber,
        //    [FromQuery] string FirstName = null,
        //    [FromQuery] string LastName = null,
        //    [FromQuery] string IdentityNumber = null)
        //{
        //    var operation = new GetOrderByParametersQuery(OrderNumber, FirstName, LastName, IdentityNumber);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("ByOrder")]
        //[Authorize(Roles = "Order")]
        //public async Task<ApiResponse<OrderResponse>> GetByOrderId()
        //{
        //    var operation = new GetOrderByOrderIdQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("{OrderId}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<OrderResponse>> Get([FromRoute] long OrderId)
        //{
        //    var operation = new GetAllOrderQuery(OrderId);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

    }
}
