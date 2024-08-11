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
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DigitalStore.Data.UnitOfWork;

namespace DigitalStore.Business.IdentityService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfig jwtConfig;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ISessionContext sessionContext;
        private readonly IMapper mapper;

        public AuthenticationService(JwtConfig jwtConfig, UserManager<User> userManager, SignInManager<User> signInManager, ISessionContext sessionContext, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtConfig = jwtConfig;
            this.sessionContext = sessionContext;
            this.roleManager = roleManager;
            this.mapper = mapper;
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
            User appUser = await userManager.GetUserAsync(sessionContext.HttpContext.User);
            if (appUser == null)
            {
                return new ApiResponse("Login Faild");
            }
            var user = await userManager.FindByNameAsync(appUser.UserName);
            if (user == null)
            {
                return new ApiResponse("Login Faild");
            }

            await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Register(RegisterUserRequest request)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                Gender = request.Gender,
                Occupation = request.Occupation,
                DateOfBirth = request.DateOfBirth,

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

        public async Task<ApiResponse> AdminRegister(RegisterAdminUserRequest request)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                Gender = request.Gender,
                Occupation = request.Occupation,
                DateOfBirth = request.DateOfBirth,


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
                var roleName = request.Role; //Admin user request ile Role ataması.
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await userManager.AddToRoleAsync(newUser, roleName);

                return new ApiResponse("User created successfully and role assigned.");
            }
            return new ApiResponse();
        }

        public async Task<string> GenerateToken(User user)
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

        private Claim[] GetClaims(User user)
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
            // Kullanıcının rollerini al ve claims listesine ekle
            var roles = userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims.ToArray();
        }

        public async Task<ApiResponse<List<UserResponse>>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var userResponses = new List<UserResponse>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var mappedUser = mapper.Map<UserResponse>(user);
                mappedUser.Role = roles.FirstOrDefault(); // Assuming a user has only one role
                userResponses.Add(mappedUser);
            }
            return new ApiResponse<List<UserResponse>>(userResponses);
        }


    }
}
