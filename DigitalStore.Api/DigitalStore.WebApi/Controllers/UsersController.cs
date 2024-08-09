using DigitalStore.Base.Response;
using DigitalStore.Business.Application.UserOperations.Commands.CreateUser;
using DigitalStore.Business.Application.UserOperations.Commands.DeleteUser;
using DigitalStore.Business.Application.UserOperations.Commands.UpdateUser;
using DigitalStore.Business.Application.UserOperations.Queries.GetUser;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<UserResponse>>> Get()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        /// <summary>
        /// Require "admin" role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest value)
        {
            var operation = new CreateUserCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Put(string UserId, [FromBody] UserRequest value)
        {
            var operation = new UpdateUserCommand(UserId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> Delete(long UserId)
        {
            var operation = new DeleteUserCommand(UserId);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet("ByParameters")]
        //[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<List<UserResponse>>> GetByParameters(
        //    [FromQuery] long? UserNumber,
        //    [FromQuery] string FirstName = null,
        //    [FromQuery] string LastName = null,
        //    [FromQuery] string IdentityNumber = null)
        //{
        //    var operation = new GetUserByParametersQuery(UserNumber, FirstName, LastName, IdentityNumber);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("ByUser")]
        //[Authorize(Roles = "User")]
        //public async Task<ApiResponse<UserResponse>> GetByUserId()
        //{
        //    var operation = new GetUserByUserIdQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        //[HttpGet("{UserId}")]
        //[Authorize(Roles = "admin")]
        //public async Task<ApiResponse<UserResponse>> Get([FromRoute] long UserId)
        //{
        //    var operation = new GetAllUserQuery(UserId);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

    }
}
