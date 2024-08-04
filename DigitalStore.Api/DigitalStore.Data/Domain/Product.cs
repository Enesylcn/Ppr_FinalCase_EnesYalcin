using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public decimal PointsEarningPercentage { get; set; }
        public decimal MaxPointsAmount { get; set; }

        // Navigation properties
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
