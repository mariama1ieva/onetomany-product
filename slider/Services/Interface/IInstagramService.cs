using slider.Models;

namespace slider.Services.Interface
{
    public interface IInstagramService
    {
        Task<List<Instagram>> GetInstagramAsync();
    }
}
