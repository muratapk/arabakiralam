using System.ComponentModel.DataAnnotations;

namespace arabakiralam.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        public string PlateNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal DailyPrice { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public int CarCategoryId { get; set; }
        public CarCategory CarCategory { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
