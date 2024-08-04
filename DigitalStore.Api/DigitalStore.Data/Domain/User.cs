using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    [Table("User", Schema = "dbo")]
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }


        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int Status { get; set; }

        public int PointCash { get; set; }
    }
}
