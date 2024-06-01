using System.ComponentModel.DataAnnotations;

namespace slider.ViewModels.Blogs
{
    public class BlogCreateVM
    {

        [Required(ErrorMessage = "Can't be empty")]
        [StringLength(20)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Can't be empty")]
        [StringLength(20)]
        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime Date { get; set; }



    }
}
