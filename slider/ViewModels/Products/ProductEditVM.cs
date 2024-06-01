namespace slider.ViewModels.Products
{
    public class ProductEditVM
    {


        public int CategoryId { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<ProductImageVM> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
