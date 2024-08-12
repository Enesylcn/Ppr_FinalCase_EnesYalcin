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
        public float PointsEarningPercentage { get; set; }
        public float MaxPointsAmount { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; } = 0;

        // Kategori ID'lerinin listesi
        public List<long> CategoryIds { get; set; }
    }


    public class ProductResponse : BaseResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public float PointsEarningPercentage { get; set; }
        public float MaxPointsAmount { get; set; }
        public float Price { get; set; }

        public int Stock { get; set; }

        // Added to include category information in the response
        public List<CategoryResponse> Categories { get; set; }
    }
}
