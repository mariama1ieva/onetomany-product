using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;
using slider.ViewModels.Blogs;

namespace slider.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {

        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.Id).ToListAsync();

            return View(blogs);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existBlogs = await _context.Blogs.AnyAsync(m => m.Title == blog.Title && m.Description == blog.Description && m.Image == blog.Image && m.Date == blog.Date);
            if (existBlogs)
            {
                ModelState.AddModelError("Title", "These inputs already exist");
            }

            await _context.Blogs.AddAsync(new Blog { Title = blog.Title, Description = blog.Description, Image = blog.Image, Date = blog.Date });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null) return BadRequest();
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null) return NotFound();

            BlogInfoVM model = new()
            {
                Title = blog.Title,
                Description = blog.Description,
                Image = blog.Image,
                Date = blog.Date

            };
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null) return NotFound();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null) return NotFound();

            return View(new BlogEditVM { Title = blog.Title, Description = blog.Description, Image = blog.Image, Date = blog.Date });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditVM blog, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            Blog existBlog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null) return NotFound();

            existBlog.Title = blog.Title;
            existBlog.Description = blog.Description;
            existBlog.Image = blog.Image;
            existBlog.Date = blog.Date;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
