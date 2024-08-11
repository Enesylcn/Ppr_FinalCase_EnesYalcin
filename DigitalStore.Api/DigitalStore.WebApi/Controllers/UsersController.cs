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
    public class UsersController : ControllerBase
    {
        //private readonly UserManager<User> userManager;
        //private readonly IMediator mediator;

        //public UsersController(IMediator mediator, UserManager<User> userManager)
        //{
        //    this.mediator = mediator;
        //    this.userManager = userManager;

        //}

        //public async Task<List<User>> GetAllUsersAsync()
        //{
        //    return await userManager.Users.ToListAsync();
        //}






        private readonly IMediator mediator;
        private readonly IAuthenticationService authenticationService;
        public UsersController(IMediator mediator, IAuthenticationService authenticationService)
        {
            this.mediator = mediator;
            this.authenticationService = authenticationService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<UserResponse>>> Get()
        {
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet]
        ////[Authorize(Roles = "Admin")]
        //public async Task<ApiResponse<List<UserResponse>>> Get()
        //{
        //    var operation = new GetAllUserQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}
        [HttpGet("{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> GetByUserId(long userId)
        {
            var operation = new GetUserByIdQuery(userId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest value)
        {
            var operation = new CreateUserCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(string userId, [FromBody] UserRequest value)
        {
            var operation = new UpdateUserCommand(userId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(long userId)
        {
            var operation = new DeleteUserCommand(userId);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet("ByParameters")]
        ////[Authorize(Roles = "Admin")]
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
        ////[Authorize(Roles = "Admin")]
        //public async Task<ApiResponse<UserResponse>> Get([FromRoute] long UserId)
        //{
        //    var operation = new GetAllUserQuery(UserId);
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

    }
}
