using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ProductOperations.Commands.CreateProduct;
using DigitalStore.Business.Application.ProductOperations.Commands.DeleteProduct;
using DigitalStore.Business.Application.ProductOperations.Commands.UpdateProduct;
using DigitalStore.Business.Application.ProductOperations.Queries.GetProduct;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<ProductResponse>>> Get()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        /// <summary>
        /// Require "admin" role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<ProductResponse>> Post([FromBody] ProductRequest value)
        {
            var operation = new CreateProductCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{ProductId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(long ProductId, [FromBody] ProductRequest value)
        {
            var operation = new UpdateProductCommand(ProductId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{ProductId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long ProductId)
        {
            var operation = new DeleteProductCommand(ProductId);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet("ByParameters")]
        //[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<List<ProductResponse>>> GetByParameters(
        //    [FromQuery] long? ProductNumber,
        //    [FromQuery] string FirstName = null,
        //    [FromQuery] string LastName = null,
        //    [FromQuery] string IdentityNumber = null)
        //{
        //    var operation = new GetProductByParametersQuery(ProductNumber, FirstName, LastName, IdentityNumber);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("ByProduct")]
        //[Authorize(Roles = "Product")]
        //public async Task<ApiResponse<ProductResponse>> GetByProductId()
        //{
        //    var operation = new GetProductByProductIdQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("{ProductId}")]
        //[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<ProductResponse>> Get([FromRoute] long ProductId)
        //{
        //    var operation = new GetAllProductQuery(ProductId);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

    }
}
