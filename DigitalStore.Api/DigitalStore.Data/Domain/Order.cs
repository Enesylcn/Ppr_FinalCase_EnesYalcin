using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class Order : BaseEntity
    {
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsAmount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation properties
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
