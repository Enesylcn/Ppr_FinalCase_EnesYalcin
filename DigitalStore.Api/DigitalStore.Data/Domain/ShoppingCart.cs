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
        public float TotalAmount { get; set; }
        public float CartAmount { get; set; }
        public string CouponCode { get; set; }
        public float PointsAmount { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
