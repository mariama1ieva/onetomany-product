using System.ComponentModel.DataAnnotations;

namespace slider.ViewModels.Products
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }

        public int CategoryId { get; set; }
        [Required]

        public string Price { get; set; }
        [Required]

        public List<IFormFile> Images { get; set; }
    }
}
