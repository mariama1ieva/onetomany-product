using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;
using slider.Services.Interface;

namespace slider.Services
{
    public class CategoryService : ICategoriyService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectList> GetALlBySelectedAsync()
        {
            var datas = await _context.Categories.ToListAsync();
            return new SelectList(datas, "Id", "Name");
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.Include(m => m.Products).Where(m => !m.SoftDeleted && m.Products.Count != 0).ToListAsync();
        }
    }
}
