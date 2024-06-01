using slider.Models;
using slider.ViewModels.Products;

namespace slider.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllWithImageAsync();
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<List<Product>> GetAllPaginateAsync(int page, int take = 4);
        Task<int> GetCountAsync();
        Task CreateAsync(Product product);

    }
}
