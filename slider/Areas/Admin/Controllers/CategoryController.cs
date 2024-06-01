using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;
using slider.ViewModels.Categories;

namespace slider.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _appDbContext.Categories.OrderByDescending(m => m.Id).ToListAsync();
            List<CategoryVM> model = categories.Select(m => new CategoryVM { Id = m.Id, Name = m.Name }).ToList();

            return View(model);

        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool existCategory = await _appDbContext.Categories.AnyAsync(m => m.Name.Trim() == category.Name.Trim());
            if (existCategory)
            {
                ModelState.AddModelError("Name", "Category already exist");
                return View();
            }
            await _appDbContext.Categories.AddAsync(new Category { Name = category.Name });
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _appDbContext.Categories.Where(m => m.Id == id).Include(m => m.Products).FirstOrDefaultAsync();

            if (category == null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = category.Name,
                Count = category.Products.Count()
            };
            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _appDbContext.Categories.Where(m => m.Id == id)
                                                               .Include(m => m.Products)
                                                               .FirstOrDefaultAsync();

            if (category == null) return NotFound();

            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _appDbContext.Categories.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (category == null) return NotFound();
            return View(new CategoryEditVM { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            if (id == null) return BadRequest();

            Category existCategory = await _appDbContext.Categories.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (category == null) return NotFound();

            existCategory.Name = category.Name;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
