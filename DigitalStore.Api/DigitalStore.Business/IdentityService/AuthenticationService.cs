using DigitalStore.Base.Response;
using DigitalStore.Base;
using DigitalStore.Base.Token;
using DigitalStore.Data.Domain;
using DigitalStore.Schema;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalStore.Business.IdentityService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfig jwtConfig;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ISessionContext sessionContext;

        public AuthenticationService(JwtConfig jwtConfig, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ISessionContext sessionContext, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtConfig = jwtConfig;
            this.sessionContext = sessionContext;
            this.roleManager = roleManager;
        }

        public async Task<ApiResponse<AuthResponse>> Login(AuthRequest request)
        {
            
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiResponse<AuthResponse>("Kullanıcı adı veya şifre hatalı!");
            }
            await signInManager.SignOutAsync();
            var isConfirmed = await userManager.IsEmailConfirmedAsync(user);
            if (!isConfirmed)
            {
                return new ApiResponse<AuthResponse>("Lütfen e-postanızı onaylayıp tekrar deneyiniz.");
            }
            var result = await signInManager.PasswordSignInAsync(user, request.Password, false, true);
            if (!result.Succeeded)
            {
                return new ApiResponse<AuthResponse>("Login Faild");
            }

            var responseToken = await GenerateToken(user);
            AuthResponse authResponse = new AuthResponse()
            {
                AccessToken = responseToken,
                UserName = request.UserName,
                ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
            };

            return new ApiResponse<AuthResponse>(authResponse);
        }

        public async Task<ApiResponse> Logout()
        {
            await signInManager.SignOutAsync();
            return new ApiResponse();
        }

        public async Task<ApiResponse> ChangePassword(ChangePasswordRequest request)
        {
            ApplicationUser applicationUser = await userManager.GetUserAsync(sessionContext.HttpContext.User);
            if (applicationUser == null)
            {
                return new ApiResponse("Login Faild");
            }
            var user = await userManager.FindByNameAsync(applicationUser.UserName);
            if (user == null)
            {
                return new ApiResponse("Login Faild");
            }

            await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Register(RegisterUserRequest request)
        {
            var newUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                
                EmailConfirmed = true,
                TwoFactorEnabled = false
            };

            var newUserResponse = await userManager.CreateAsync(newUser, request.Password);
            if (!newUserResponse.Succeeded)
            {
                foreach (var error in newUserResponse.Errors)
                {
                    return new ApiResponse(error.Description);
                }
            }
            else
            {
                var roleName = "Customer";
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await userManager.AddToRoleAsync(newUser, roleName);

                return new ApiResponse("User created successfully and role assigned.");
            }
            return new ApiResponse();
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        private Claim[] GetClaims(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>()
        {
            new Claim("UserName", user.UserName),
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
        };

            return claims.ToArray();
        }
    }
}
