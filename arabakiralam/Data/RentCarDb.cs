using arabakiralam.Models;
using Microsoft.EntityFrameworkCore;

namespace arabakiralam.Data
{
    public class RentCarDb : DbContext
    {
        public RentCarDb(DbContextOptions<RentCarDb> options) : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; } 
        public DbSet<CarImages> CarImages { get; set; }
    }
}
