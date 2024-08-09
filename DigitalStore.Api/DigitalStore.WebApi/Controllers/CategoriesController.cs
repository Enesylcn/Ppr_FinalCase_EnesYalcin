using DigitalStore.Base.Response;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        // Sadece giriş yapmış kullanıcılar buraya erişebilir.
        [HttpGet("protected-endpoint")]
        public IActionResult GetProtectedData()
        {
            return Ok("This is protected data.");
        }
    }
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize]
    //public class CategoriesController : ControllerBase
    //{
    //    private readonly IMediator mediator;

    //    public CategoriesController(IMediator mediator)
    //    {
    //        this.mediator = mediator;
    //    }


    //    [HttpGet("protected-endpoint")]
    //    //[Authorize(Roles = "Admin")]
    //    public async Task<ApiResponse<List<CategoryResponse>>> Get()
    //    {
    //        var operation = new GetAllCategoryQuery();
    //        var result = await mediator.Send(operation);
    //        return result;
    //    }

    //    [HttpGet("{CategoryId}")]
    //    ////[Authorize(Roles = "Admin")]
    //    public async Task<ApiResponse<CategoryResponse>> Get([FromRoute] long categoryId)
    //    {
    //        var operation = new GetCategoryByIdQuery(categoryId);
    //        var result = await mediator.Send(operation);
    //        return result;
    //    }

    //    [HttpPost]
    //    ////[Authorize(Roles = "Admin")]
    //    public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest value)
    //    {
    //        var operation = new CreateCategoryCommand(value);
    //        var result = await mediator.Send(operation);
    //        return result;
    //    }

    //    [HttpPut("{CategoryId}")]
    //    ////[Authorize(Roles = "Admin")]
    //    public async Task<ApiResponse> Put(long CategoryId, [FromBody] CategoryRequest value)
    //    {
    //        var operation = new UpdateCategoryCommand(CategoryId, value);
    //        var result = await mediator.Send(operation);
    //        return result;
    //    }

    //    [HttpDelete("{CategoryId}")]
    //    //[Authorize(Roles = "Admin")]
    //    public async Task<ApiResponse> Delete(long CategoryId)
    //    {
    //        var operation = new DeleteCategoryCommand(CategoryId);
    //        var result = await mediator.Send(operation);
    //        return result;
    //    }

    //    //[HttpGet("ByParameters")]
    //    ////[Authorize(Roles = "Admin")]
    //    //public async Task<ApiResponse<List<CategoryResponse>>> GetByParameters(
    //    //    [FromQuery] long? CategoryNumber,
    //    //    [FromQuery] string FirstName = null,
    //    //    [FromQuery] string LastName = null,
    //    //    [FromQuery] string IdentityNumber = null)
    //    //{
    //    //    var operation = new GetCategoryByParametersQuery(CategoryNumber, FirstName, LastName, IdentityNumber);
    //    //    var result = await mediator.Send(operation);
    //    //    return result;
    //    //}

    //    //[HttpGet("ByCategory")]
    //    //[Authorize(Roles = "Category")]
    //    //public async Task<ApiResponse<CategoryResponse>> GetByCategoryId()
    //    //{
    //    //    var operation = new GetCategoryByCategoryIdQuery();
    //    //    var result = await mediator.Send(operation);
    //    //    return result;
    //    //}

    //}
}
