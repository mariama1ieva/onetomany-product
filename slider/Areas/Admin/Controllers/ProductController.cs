using Microsoft.AspNetCore.Mvc;
using slider.Data;
using slider.Helpers.Extentions;
using slider.Models;
using slider.Services.Interface;
using slider.ViewModels.Products;

namespace slider.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProductService _productService;
        private readonly ICategoriyService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService, AppDbContext appDbContext,
                                 ICategoriyService categoriyService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _appDbContext = appDbContext;
            _categoryService = categoriyService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var dbProduct = await _productService.GetAllPaginateAsync(page);

            List<ProductVM> mappedDatas = _productService.GetMappedDatas(dbProduct);
            int pageCount = await GetPageCountAsync(4);

            Paginate<ProductVM> model = new(mappedDatas, pageCount, page);

            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)count / take);
        }


        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();

            List<ProductImageVM> productImages = new();
            foreach (var item in product.ProductImages)
            {
                productImages.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });

            }

            ProductDetailVM model = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category.Name,
                Images = productImages
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categories = await _categoryService.GetALlBySelectedAsync();

            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();


            List<ProductImageVM> productImage = new();
            foreach (var item in product.ProductImages)
            {
                productImage.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }


            return View(new ProductEditVM { Name = product.Name, Description = product.Description, Price = product.Price, Images = productImage, Category = product.Category.Name });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditVM productEditVM, int? id)
        {

            if (ModelState.IsValid) return View();
            if (id == null) return BadRequest();
            Product existProduct = await _productService.GetByIdAsync((int)id);

            if (existProduct == null) return NotFound();

            existProduct.Name = productEditVM.Name;
            existProduct.Description = productEditVM.Description;
            existProduct.Price = productEditVM.Price;
            existProduct.CategoryId = productEditVM.CategoryId;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetALlBySelectedAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            ViewBag.Categories = await _categoryService.GetALlBySelectedAsync();
            if (!ModelState.IsValid) return View();

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500 kb");
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "Image format must be img");
                    return View();
                }
            }
            List<ProductImage> images = new();

            foreach (var item in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                await item.SaveFileToLocalAsync(path);


                images.Add(new ProductImage
                {
                    Name = fileName
                });
            }

            images.FirstOrDefault().IsMain = true;
            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = decimal.Parse(request.Price),
                CategoryId = request.CategoryId,
                ProductImages = images
            };

            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));

        }


    }
}
