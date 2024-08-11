using DigitalStore.Base.Response;
using DigitalStore.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.IdentityService
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<AuthResponse>> Login(AuthRequest request);
        Task<ApiResponse> Logout();
        Task<ApiResponse> ChangePassword(ChangePasswordRequest request);
        Task<ApiResponse> Register(RegisterUserRequest request);
        Task<ApiResponse<List<UserResponse>>> GetAllUsersAsync();

    }
}
