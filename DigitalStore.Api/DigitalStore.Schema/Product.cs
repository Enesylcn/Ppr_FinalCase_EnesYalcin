using DigitalStore.Base.Entity;
using DigitalStore.Base.Schema;
using DigitalStore.Data.Domain;

namespace DigitalStore.Schema
{
    public class ProductRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public decimal PointsEarningPercentage { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;
    }


    public class ProductResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public decimal PointsEarningPercentage { get; set; }
        public decimal MaxPointsAmount { get; set; }
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
