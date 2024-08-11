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
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<OrderDetailResponse>>> Get()
        {
            var operation = new GetAllOrderDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        /// <summary>
        /// Require "admin" role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<OrderDetailResponse>> Post([FromBody] OrderDetailRequest value)
        {
            var operation = new CreateOrderDetailCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{OrderDetailId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(long OrderDetailId, [FromBody] OrderDetailRequest value)
        {
            var operation = new UpdateOrderDetailCommand(OrderDetailId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{OrderDetailId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(long OrderDetailId)
        {
            var operation = new DeleteOrderDetailCommand(OrderDetailId);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet("ByParameters")]
        ////[Authorize(Roles = "Admin")]
        //public async Task<ApiResponse<List<OrderDetailResponse>>> GetByParameters(
        //    [FromQuery] long? OrderDetailNumber,
        //    [FromQuery] string FirstName = null,
        //    [FromQuery] string LastName = null,
        //    [FromQuery] string IdentityNumber = null)
        //{
        //    var operation = new GetOrderDetailByParametersQuery(OrderDetailNumber, FirstName, LastName, IdentityNumber);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("ByOrderDetail")]
        //[Authorize(Roles = "OrderDetail")]
        //public async Task<ApiResponse<OrderDetailResponse>> GetByOrderDetailId()
        //{
        //    var operation = new GetOrderDetailByOrderDetailIdQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("{OrderDetailId}")]
        ////[Authorize(Roles = "Admin")]
        //public async Task<ApiResponse<OrderDetailResponse>> Get([FromRoute] long OrderDetailId)
        //{
        //    var operation = new GetAllOrderDetailQuery(OrderDetailId);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

    }
}
