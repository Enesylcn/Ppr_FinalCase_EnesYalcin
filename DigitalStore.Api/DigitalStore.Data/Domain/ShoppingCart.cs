using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string UserId { get; set; }
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsAmount { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
