using Microsoft.AspNetCore.Identity;

namespace arabakiralam.Models
{
    public class AppUser:IdentityUser
    {
        public string AdSoyad { get; set; } 
        public string Country {  get; set; }
    }
}
