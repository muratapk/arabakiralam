using arabakiralam.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace arabakiralam.Models
{
    public class CarImages
    {
        [Key]
        public int CarImagesId { get; set; }
        //primary CarImagesId
        public string CarImagesName { get; set; }=string.Empty;
        public string CarImagesUrl { get; set; } = string.Empty;
        public int ? CarId { get; set; }
        public Car ? Car { get; set; }
        [NotMapped]
        public IFormFile ? ImageFile {  get; set; }

    }
}
