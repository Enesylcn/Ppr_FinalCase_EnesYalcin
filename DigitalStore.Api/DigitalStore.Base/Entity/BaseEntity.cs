
namespace DigitalStore.Base.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null;
        public string InsertUser { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
