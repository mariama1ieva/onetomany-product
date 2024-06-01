using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slider.Data;
using slider.Models;

namespace slider.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = new SliderVMVC()
            {
                Sliders = await _context.Sliders.ToListAsync(),
                SliderInfo = await _context.SliderInfos.FirstOrDefaultAsync()

            };

            return await Task.FromResult(View(datas));

        }
    }


    public class SliderVMVC
    {
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
