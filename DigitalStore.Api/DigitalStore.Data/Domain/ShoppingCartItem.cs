using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class ShoppingCartItem : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; } // Adet
        public decimal Price { get; set; } // Adet
    }
}
