using Microsoft.Identity.Client;
using arabakiralam.Models;
using System.ComponentModel.DataAnnotations;

namespace arabakiralam.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        public string PlateNumber { get; set; } =string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ? Year { get; set; }
        public decimal ? DailyPrice { get; set; }

        public bool IsAvailable { get; set; } = false;

        public int ? BrandId { get; set; }
        public Brand ? Brand { get; set; } = null!;

        public int ? CarCategoryId { get; set; }
        public CarCategory ? CarCategory { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<CarImages> CarImages{ get; set; } = new List<CarImages>();
    }
}
