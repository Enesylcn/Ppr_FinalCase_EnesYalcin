using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CategoryOperations.Queries.GetCategory;
using DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder;
using DigitalStore.Business.Application.ProductOperations.Queries.GetProduct;
using DigitalStore.Business.Application.ShoppingCartOperations.Commands.CreateShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.AddToCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCart;
using DigitalStore.Business.Application.ShoppingCartOperations.Queries.GetShoppingCartByUserId;
using DigitalStore.Data.Domain;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Customer")]

    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ISessionContext sessionContext;

        public CustomerController(IMediator mediator, ISessionContext sessionContext)
        {
            this.mediator = mediator;
            this.sessionContext = sessionContext;
        }


        [HttpGet("products")]
        public async Task<ApiResponse<List<ProductResponse>>> ProductList()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("carts")]
        public async Task<ApiResponse<List<ShoppingCartResponse>>> CartList()
        {
            var operation = new GetAllCartQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("cart")]
        public async Task<ApiResponse<ShoppingCart>> AddToCart([FromBody] ShoppingCartRequest value)
        {

            var operation = new AddToCartQuery(sessionContext.Session.UserId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("payment")]
        public async Task<ApiResponse> Payment([FromBody] OrderRequest value)
        {
            var operation = new CreateOrderCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
