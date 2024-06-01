using Microsoft.Build.Framework;

namespace slider.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> Names { get; set; }
    }
}
