using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string IdentityNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
