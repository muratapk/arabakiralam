using System.ComponentModel.DataAnnotations;

namespace arabakiralam.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
