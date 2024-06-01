using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using slider.Data;
using slider.Models;
using slider.Services.Interface;
using slider.ViewModels;
using slider.ViewModels.Baskets;

namespace slider.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoriyService _categoriyService;
        private readonly IExpertService _expertService;
        private readonly IExpertSliderService _expertSliderService;
        private readonly IInstagramService _instagramService;
        private readonly IHttpContextAccessor _contextAccessor;



        public HomeController(AppDbContext context, IProductService productService,
            ICategoriyService categoriyService, IExpertService expertService, IExpertSliderService expertSliderService,
            IInstagramService instagramService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _productService = productService;
            _categoriyService = categoriyService;
            _expertService = expertService;
            _expertSliderService = expertSliderService;
            _instagramService = instagramService;
            _contextAccessor = httpContextAccessor;



        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoriyService.GetCategoriesAsync();
            List<Product> products = await _productService.GetAllAsync();
            AboutSuprise aboutSuprise = await _context.AboutSuprise.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
            SupriseText supriseText = await _context.SupriseTexts.FirstOrDefaultAsync();
            List<Expert> experts = await _expertService.GetExpertsAsync();
            List<Expert> expertSliders = await _expertSliderService.GetExpertSlidersAsync();
            List<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDeleted).Take(3).ToListAsync();
            List<Instagram> instagrams = await _instagramService.GetInstagramAsync();

            HomeVM model = new()
            {

                Categories = categories,
                Products = products,
                AboutSuprise = aboutSuprise,
                SupriseText = supriseText,
                Experts = experts,
                ExpertSliders = expertSliders,
                Blogs = blogs,
                Instagrams = instagrams
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(int? id)
        {
            if (id == null) return BadRequest();
            List<BasketVM> basketProduct = null;

            var basketRequest = _contextAccessor.HttpContext.Request.Cookies["basket"];


            if (basketRequest is not null)
            {
                basketProduct = JsonConvert.DeserializeObject<List<BasketVM>>(basketRequest);
            }
            else
            {
                basketProduct = new List<BasketVM>();
            }

            var existProduct = basketProduct.FirstOrDefault(m => m.Id == id);
            var productPrice = _context.Products.FirstOrDefault(m => m.Id == id);
            if (existProduct != null)
            {
                existProduct.Count++;
            }
            else
            {
                basketProduct.Add(new BasketVM
                {
                    Id = (int)id,
                    Count = 1,
                    Price = productPrice.Price

                });
            }




            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProduct));

            var count = basketProduct.Sum(m => m.Count);
            var total = basketProduct.Sum(m => m.Count * m.Price);

            return Ok(new { count, total });




        }


    }
}
