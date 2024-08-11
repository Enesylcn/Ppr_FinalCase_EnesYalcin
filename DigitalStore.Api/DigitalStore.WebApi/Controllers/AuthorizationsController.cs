using DigitalStore.Base.Response;
using DigitalStore.Business.IdentityService;
using DigitalStore.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.WebApi.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthorizationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ApiResponse<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            var loginResult = await authenticationService.Login(request);
            return loginResult;
        }

        [HttpPost("Logout")]
        [AllowAnonymous]
        public async Task<ApiResponse> Logout()
        {
            var response = await authenticationService.Logout();
            return response;
        }

        [HttpPost("ChangePassword")]
        [AllowAnonymous]
        public async Task<ApiResponse> Logout([FromBody] ChangePasswordRequest request)
        {
            var response = await authenticationService.ChangePassword(request);
            return response;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ApiResponse> Register([FromBody] RegisterUserRequest request)
        {
            var response = await authenticationService.Register(request);
            return response;
        }

        [HttpPost("AdminRegister")]
        [AllowAnonymous]
        public async Task<ApiResponse> AdminRegister([FromBody] RegisterAdminUserRequest request)
        {
            var response = await authenticationService.AdminRegister(request);
            return response;
        }

        [HttpPost("GetAllUsersAsync")]
        [AllowAnonymous]
        public async Task<ApiResponse<List<UserResponse>>> GetAllUsersAsync()
        {
            var loginResult = await authenticationService.GetAllUsersAsync();
            return loginResult;
        }

    }
}
