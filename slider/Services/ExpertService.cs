using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;
using slider.Services.Interface;

namespace slider.Services
{
    public class ExpertService : IExpertService
    {
        private readonly AppDbContext _context;
        public ExpertService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Expert>> GetExpertsAsync()
        {
            return await _context.Experts.Where(m => !m.SoftDeleted).ToListAsync();
        }
    }
}
