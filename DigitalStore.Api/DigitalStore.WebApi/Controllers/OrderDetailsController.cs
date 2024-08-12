using DigitalStore.Base.Response;
using DigitalStore.Business.Application.OrderDetailOperations.Commands.CreateOrderDetail;
using DigitalStore.Business.Application.OrderDetailOperations.Commands.DeleteOrderDetail;
using DigitalStore.Business.Application.OrderDetailOperations.Commands.UpdateOrderDetail;
using DigitalStore.Business.Application.OrderDetailOperations.Queries.GetOrderDetail;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class OrderDetailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<OrderDetailResponse>>> Get()
        {
            var operation = new GetAllOrderDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<OrderDetailResponse>> Post([FromBody] OrderDetailRequest value)
        {
            var operation = new CreateOrderDetailCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{OrderDetailId}")]
        public async Task<ApiResponse> Put(long OrderDetailId, [FromBody] OrderDetailRequest value)
        {
            var operation = new UpdateOrderDetailCommand(OrderDetailId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{OrderDetailId}")]
        public async Task<ApiResponse> Delete(long OrderDetailId)
        {
            var operation = new DeleteOrderDetailCommand(OrderDetailId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
