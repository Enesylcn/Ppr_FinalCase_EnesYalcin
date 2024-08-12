using DigitalStore.Base.Schema;
using DigitalStore.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{
    public class ShoppingCartRequest : BaseRequest
    {
        public List<long> ProductIds { get; set; } // Birden fazla ürün ID'si
        public int Quantity { get; set; }
        public string CouponCode { get; set; }
    }

    public class ShoppingCartResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public float CartAmount { get; set; }
        public string CouponCode { get; set; }
        public float PointsAmount { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
