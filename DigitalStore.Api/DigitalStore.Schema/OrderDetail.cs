using DigitalStore.Base.Entity;
using DigitalStore.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{
    public class OrderDetailRequest : BaseRequest
    {
        public long Id { get; set; }

    }


    public class OrderDetailResponse : BaseResponse
    {
        public long Id { get; set; }

        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
