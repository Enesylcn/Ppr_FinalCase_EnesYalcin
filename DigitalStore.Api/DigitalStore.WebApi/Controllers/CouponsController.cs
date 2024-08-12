using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CouponOperations.Commands.CreateCoupon;
using DigitalStore.Business.Application.CouponOperations.Commands.DeleteCoupon;
using DigitalStore.Business.Application.CouponOperations.Commands.UpdateCoupon;
using DigitalStore.Business.Application.CouponOperations.Queries.GetCoupon;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CouponsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CouponsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CouponResponse>>> Get()
        {
            var operation = new GetAllCouponQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CouponResponse>> Post([FromBody] CouponRequest value)
        {
            var operation = new CreateCouponCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{CouponId}")]
        public async Task<ApiResponse> Put(long CouponId, [FromBody] CouponRequest value)
        {
            var operation = new UpdateCouponCommand(CouponId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{CouponId}")]
        public async Task<ApiResponse> Delete(long CouponId)
        {
            var operation = new DeleteCouponCommand(CouponId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
