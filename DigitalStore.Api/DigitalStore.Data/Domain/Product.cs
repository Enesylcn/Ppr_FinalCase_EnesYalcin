using DigitalStore.Base.Entity;

namespace DigitalStore.Data.Domain
{
    public class Product : BaseEntity
    {
        public string Features { get; set; }
        public string Description { get; set; }
        public decimal PointsEarningPercentage { get; set; }
        public decimal MaxPointsAmount { get; set; }
        public int Stock { get; set; }
        

        // Navigation properties
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
