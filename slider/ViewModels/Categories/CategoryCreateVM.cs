using System.ComponentModel.DataAnnotations;

namespace slider.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Can't be empty")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
