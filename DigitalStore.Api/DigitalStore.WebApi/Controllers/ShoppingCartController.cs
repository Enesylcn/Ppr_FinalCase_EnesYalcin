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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ApiResponse<List<ShoppingCartResponse>>> Get()
        {
            var operation = new GetAllCartQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{shoppingCartId}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]

        public async Task<ApiResponse> Put(long shoppingCartId, [FromBody] ShoppingCartRequest value)
        {
            var operation = new UpdateCartCommand(shoppingCartId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{shoppingCartId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ApiResponse> Delete(long shoppingCartId)
        {
            var operation = new DeleteCartCommand(shoppingCartId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
