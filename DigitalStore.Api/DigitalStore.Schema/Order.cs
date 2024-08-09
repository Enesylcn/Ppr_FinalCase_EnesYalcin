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
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }


    public class OrderResponse : BaseResponse
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public decimal EarnedBonusAmount { get; set; }
        public decimal CartAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal SpentsBonusAmount { get; set; }

    }
}
