using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Helpers.Extentions;
using slider.Models;
using slider.ViewModels.Slider;

namespace slider.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {



        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<SliderVM> slidersVM = sliders.Select(m => new SliderVM { Id = m.Id, Name = m.Name }).ToList();
            return View(slidersVM);
        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            foreach (var item in request.Names)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Name", "File must be only image format");
                    return View();
                }

                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Name", "File size must be max 200kb");
                    return View();
                }
            }

            foreach (var item in request.Names)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                await item.SaveFileToLocalAsync(path);

                await _context.Sliders.AddAsync(new Slider { Name = fileName });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null) return NotFound();

            SliderDetailVM model = new()
            {
                Name = slider.Name

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.Name);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null) return NotFound();

            return View(new SliderEditVM { Name = slider.Name });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null) return NotFound();


            if (request.NewImage == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "This must be image format");
                request.Name = slider.Name;

                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {

                ModelState.AddModelError("NewImage", "This must be 200 kb ");

                request.Name = slider.Name;
                return View(request);
            }

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.Name);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + " " + request.NewImage.FileName;

            string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);

            slider.Name = fileName;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }




    }
}
