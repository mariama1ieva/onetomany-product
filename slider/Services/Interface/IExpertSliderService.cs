using slider.Models;

namespace slider.Services.Interface
{
    public interface IExpertSliderService
    {
        Task<List<Expert>> GetExpertSlidersAsync();
    }
}
