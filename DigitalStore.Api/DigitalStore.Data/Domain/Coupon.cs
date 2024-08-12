using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public float DiscountPercentage { get; set; } = 15; // Yüzde 15 indirim
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

    }
}
