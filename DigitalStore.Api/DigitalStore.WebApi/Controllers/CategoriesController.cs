﻿using DigitalStore.Base.Response;
using DigitalStore.Business.Application.CategoryOperations.Commands.CreateCategory;
using DigitalStore.Business.Application.CategoryOperations.Commands.DeleteCategory;
using DigitalStore.Business.Application.CategoryOperations.Commands.UpdateCategory;
using DigitalStore.Business.Application.CategoryOperations.Queries.GetCategory;
using DigitalStore.Business.Application.CategoryOperations.Queries.GetCategoryDetails;
using DigitalStore.Business.Application.ProductOperations.Queries.GetProductDetails;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CategoryResponse>>> Get()
        {
            var operation = new GetAllCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{categoryId}")]
        public async Task<ApiResponse<CategoryResponse>> Get([FromRoute] long categoryId)
        {
            var operation = new GetCategoryByIdQuery(categoryId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest value)
        {
            var operation = new CreateCategoryCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{CategoryId}")]
        public async Task<ApiResponse> Put(long CategoryId, [FromBody] CategoryRequest value)
        {
            var operation = new UpdateCategoryCommand(CategoryId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{CategoryId}")]
        public async Task<ApiResponse> Delete(long CategoryId)
        {
            var operation = new DeleteCategoryCommand(CategoryId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
