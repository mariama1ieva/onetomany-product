using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using slider.Services.Interface;
using slider.ViewModels;
using slider.ViewModels.Baskets;

namespace slider.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HeaderViewComponent(ISettingService settingService,

            IHttpContextAccessor contextAccessor)
        {
            _settingService = settingService;
            _contextAccessor = contextAccessor;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _settingService.GetALLAsync();

            List<BasketVM> basketProduct = new();

            var basketRequest = _contextAccessor.HttpContext.Request.Cookies["basket"];


            if (basketRequest is not null)
            {
                basketProduct = JsonConvert.DeserializeObject<List<BasketVM>>(basketRequest);
            }
            HeaderVM headerVM = new()
            {
                Settings = settings,
                BasketCount = basketProduct.Sum(x => x.Count),
                BasketTotalPrice = basketProduct.Sum(x => x.Count * x.Price)
            };

            return await Task.FromResult(View(headerVM));

        }
    }
}
