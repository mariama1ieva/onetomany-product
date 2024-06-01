using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;
using slider.Services.Interface;

namespace slider.Services
{
    public class ExpertSliderService : IExpertSliderService
    {
        private readonly AppDbContext _context;
        public ExpertSliderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Expert>> GetExpertSlidersAsync()
        {
            return await _context.Experts.Where(m => !m.SoftDeleted).Skip(2).ToListAsync();
        }
    }
}
