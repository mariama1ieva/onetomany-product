using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;

namespace slider.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var count = await _context.Blogs.CountAsync();
            ViewBag.Count = count;
            List<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDeleted).Take(3).ToListAsync();

            return View(blogs);
        }



        [HttpGet]
        public async Task<IActionResult> SHowMore(int skip)
        {
            List<Blog> blogs = await _context.Blogs.Skip(skip).Take(3).ToListAsync();
            return PartialView("_blogPartial", blogs);
        }


    }
}
