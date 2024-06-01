using slider.Models;

namespace slider.Services.Interface
{
    public interface IExpertService
    {
        Task<List<Expert>> GetExpertsAsync();
    }
}
