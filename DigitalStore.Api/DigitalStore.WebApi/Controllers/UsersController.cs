using DigitalStore.Base.Response;
using DigitalStore.Business.Application.UserOperations.Commands.CreateUser;
using DigitalStore.Business.Application.UserOperations.Commands.DeleteUser;
using DigitalStore.Business.Application.UserOperations.Commands.UpdateUser;
using DigitalStore.Business.Application.UserOperations.Queries.GetUser;
using DigitalStore.Business.Application.UserOperations.Queries.GetUserDetails;
using DigitalStore.Business.IdentityService;
using DigitalStore.Data.Domain;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly IAuthenticationService authenticationService;
        public UsersController(IMediator mediator, IAuthenticationService authenticationService)
        {
            this.mediator = mediator;
            this.authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<UserResponse>>> Get()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }

       
        [HttpGet("{userId}")]
        public async Task<ApiResponse<UserResponse>> GetByUserId(long userId)
        {
            var operation = new GetUserByIdQuery(userId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest value)
        {
            var operation = new CreateUserCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{userId}")]
        public async Task<ApiResponse> Put(string userId, [FromBody] UserRequest value)
        {
            var operation = new UpdateUserCommand(userId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{userId}")]
        public async Task<ApiResponse> Delete(long userId)
        {
            var operation = new DeleteUserCommand(userId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
