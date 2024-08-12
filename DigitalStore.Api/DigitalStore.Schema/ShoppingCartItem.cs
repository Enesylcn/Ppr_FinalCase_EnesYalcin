using DigitalStore.Base.Schema;
using DigitalStore.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{
    public class ShoppingCartItemRequest : BaseRequest
    {
        public long ProductId { get; set; }
        public long ShoppingCartId { get; set; }
    }
    public class ShoppingCartItemResponse : BaseResponse
    {
        public long Id { get; set;}
        public Product Product { get; set; }


    }
}
