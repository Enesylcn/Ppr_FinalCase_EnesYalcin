using DigitalStore.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }

        // Navigation properties
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
