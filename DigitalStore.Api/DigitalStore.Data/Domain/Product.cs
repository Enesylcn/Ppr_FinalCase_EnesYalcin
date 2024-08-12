using DigitalStore.Base.Entity;

namespace DigitalStore.Data.Domain
{
    public class Product : BaseEntity
    {
        public string Features { get; set; }
        public string Description { get; set; }
        public float PointsEarningPercentage { get; set; }
        public float MaxPointsAmount { get; set; }
        public float Price { get; set; }

        public int Stock { get; set; }
        

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
