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
        public string Name { get; set; }
        public string CouponCode { get; set; }
    }

    public class ShoppingCartResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsAmount { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
