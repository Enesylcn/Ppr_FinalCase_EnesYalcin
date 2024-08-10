using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Base
{
    public class JwtManager
    {
        public static Session GetSession(HttpContext context)
        {
            Session session = new Session();
            var identity = context.User.Identity as ClaimsIdentity;
            var claims = identity.Claims;
            session.UserName = GetClaimValue(claims, "UserName");
            session.Status = Convert.ToInt32(GetClaimValue(claims, "Status"));
            session.UserId = GetClaimValue(claims, "UserId");
            session.Role = GetClaimValue(claims, "Role");
            session.Email = GetClaimValue(claims, "Email");
            return session;
        }

        private static string GetClaimValue(IEnumerable<Claim> claims, string name)
        {
            var claim = claims.FirstOrDefault(c => c.Type == name);
            return claim?.Value;
        }
    }
}
