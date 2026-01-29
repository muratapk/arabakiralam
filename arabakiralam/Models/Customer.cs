using System.ComponentModel.DataAnnotations;

namespace arabakiralam.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name ="Adınız")]
        [Required(ErrorMessage ="Ad alanı zorunludur.")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string LastName { get; set; } = null!;
        [Display(Name = "E-posta Adresiniz")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = null!;
        [Display(Name = "Telefon Numaranız")]
        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string Phone { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
