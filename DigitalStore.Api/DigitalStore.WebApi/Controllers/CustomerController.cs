using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CategoryOperations.Queries.GetCategory;
using DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder;
using DigitalStore.Business.Application.ProductOperations.Queries.GetProduct;
using DigitalStore.Business.Application.ShoppingCartOperations.Commands.CreateShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCart;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("products")]
        // [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ProductResponse>>> ProductList()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("carts")]
        // [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ShoppingCartResponse>>> CartList()
        {
            var operation = new GetAllCartQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("cart")]
        // [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ShoppingCartResponse>> AddToCart([FromBody] ShoppingCartRequest value)
        {
            var operation = new CreateCartCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("payment")]
        // [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<OrderResponse>> Payment([FromBody] OrderRequest value)
        {
            var operation = new CreateOrderCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
