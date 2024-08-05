using DigitalStore.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{
    public class AuthRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class AuthResponse : BaseResponse
    {
        public DateTime ExpireTime { get; set; }
        public string AccessToken { get; set; }
        public string UserName { get; set; }
    }

    public class ChangePasswordRequest : BaseRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }


    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
