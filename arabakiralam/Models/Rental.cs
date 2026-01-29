using System.ComponentModel.DataAnnotations;

namespace arabakiralam.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
