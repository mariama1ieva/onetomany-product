using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using slider.Data;
using slider.ViewModels.Baskets;

namespace slider.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CartController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketProduct = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProduct = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }


            var products = await _context.Products.Include(m => m.Category)
                                                  .Include(m => m.ProductImages)
                                                   .ToListAsync();


            List<BasketProductVM> basket = new();

            foreach (var item in basketProduct)
            {
                var dbProduct = products.FirstOrDefault(products => products.Id == item.Id);
                basket.Add(new BasketProductVM()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    Category = dbProduct.Category.Name,
                    Count = item.Count,
                    Image = dbProduct.ProductImages.FirstOrDefault(m => m.IsMain).Name
                });
            }


            CartVM response = new()
            {
                BasketProducts = basket,
                Total = basketProduct.Sum(m => m.Count * m.Price)
            };
            return View(response);
        }

        [HttpPost]
        public IActionResult DeleteProductFromBasket(int? id)
        {
            if (id == null) return BadRequest();
            List<BasketVM> basketProducts = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            basketProducts = basketProducts.Where(m => m.Id != id).ToList();
            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);

            return Ok(new { count, total });
        }
    }
}
