using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ShoppingCartOperations.Commands.CreateShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Commands.DeleteShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Commands.UpdateShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCartDetails;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShoppingCartController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ShoppingCartResponse>>> Get()
        {
            var operation = new GetAllCartQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{shoppingCartId}")]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse<ShoppingCartResponse>> Get([FromRoute] long shoppingCartId)
        {
            var operation = new GetCartByIdQuery(shoppingCartId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ShoppingCartResponse>> Post([FromBody] ShoppingCartRequest value)
        {
            var operation = new CreateCartCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{shoppingCartId}")]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long shoppingCartId, [FromBody] ShoppingCartRequest value)
        {
            var operation = new UpdateCartCommand(shoppingCartId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{shoppingCartId}")]
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long shoppingCartId)
        {
            var operation = new DeleteCartCommand(shoppingCartId);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet("ByParameters")]
        ////[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<List<ShoppingCartResponse>>> GetByParameters(
        //    [FromQuery] long? ShoppingCartNumber,
        //    [FromQuery] string FirstName = null,
        //    [FromQuery] string LastName = null,
        //    [FromQuery] string IdentityNumber = null)
        //{
        //    var operation = new GetShoppingCartByParametersQuery(ShoppingCartNumber, FirstName, LastName, IdentityNumber);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("ByShoppingCart")]
        //[Authorize(Roles = "ShoppingCart")]
        //public async Task<ApiResponse<ShoppingCartResponse>> GetByShoppingCartId()
        //{
        //    var operation = new GetShoppingCartByShoppingCartIdQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("{ShoppingCartId}")]
        ////[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<ShoppingCartResponse>> Get([FromRoute] long ShoppingCartId)
        //{
        //    var operation = new GetAllShoppingCartQuery(ShoppingCartId);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

    }
}
