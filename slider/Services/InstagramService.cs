using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;
using slider.Services.Interface;

namespace slider.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly AppDbContext _context;
        public InstagramService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Instagram>> GetInstagramAsync()
        {
            return await _context.Instagrams.Where(m => !m.SoftDeleted).ToListAsync();
        }
    }
}
