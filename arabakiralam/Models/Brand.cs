namespace arabakiralam.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
