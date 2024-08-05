using DigitalStore.Base.Entity;
using DigitalStore.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{
    public class OrderRequest : BaseRequest
    {
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsAmount { get; set; }

        public int UserId { get; set; }
    }


    public class OrderResponse : BaseResponse
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsAmount { get; set; }

    }
}
