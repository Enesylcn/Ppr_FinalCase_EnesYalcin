﻿using DigitalStore.Base.Response;
using DigitalStore.Business.Application.ProductOperations.Commands.CreateProduct;
using DigitalStore.Business.Application.ProductOperations.Commands.DeleteProduct;
using DigitalStore.Business.Application.ProductOperations.Commands.UpdateProduct;
using DigitalStore.Business.Application.ProductOperations.Queries.GetOnsaleProduct;
using DigitalStore.Business.Application.ProductOperations.Queries.GetProduct;
using DigitalStore.Business.Application.ProductOperations.Queries.GetProductDetails;
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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ApiResponse<List<ProductResponse>>> Get()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{productId}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ApiResponse<ProductResponse>> Get([FromRoute] long productId)
        {
            var operation = new GetProductByIdQuery(productId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("onsale/{onSale}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ApiResponse<List<ProductResponse>>> Get([FromRoute] bool onSale)
        {
            var operation = new GetOnSaleProductQuery(onSale);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ProductResponse>> Post([FromBody] ProductRequest value)
        {
            var operation = new CreateProductCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{ProductId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ApiResponse> Put(long ProductId, [FromBody] ProductRequest value)
        {
            var operation = new UpdateProductCommand(ProductId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{ProductId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ApiResponse> Delete(long ProductId)
        {
            var operation = new DeleteProductCommand(ProductId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
