using System.ComponentModel.DataAnnotations;

namespace slider.ViewModels.Categories
{
    public class CategoryEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
