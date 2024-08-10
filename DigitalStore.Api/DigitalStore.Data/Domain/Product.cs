using DigitalStore.Base.Entity;

namespace DigitalStore.Data.Domain
{
    public class Product : BaseEntity
    {
        public string Features { get; set; }
        public string Description { get; set; }
        public decimal PointsEarningPercentage { get; set; }
        public decimal MaxPointsAmount { get; set; }
        public decimal Price { get; set; }

        public int Stock { get; set; }
        

        public List<ProductCategory> ProductCategories { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
