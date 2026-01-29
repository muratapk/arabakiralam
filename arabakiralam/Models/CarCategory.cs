using System.ComponentModel.DataAnnotations;

namespace arabakiralam.Models
{
    public class CarCategory
    {
        [Key]
        public int CarCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
