using Microsoft.EntityFrameworkCore;
using slider.Models;

namespace slider.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<AboutSuprise> AboutSuprise { get; set; }
        public DbSet<SupriseText> SupriseTexts { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }
        public DbSet<Setting> Settings { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasData(

                new Blog { Id = 1, Title = "Blog1", Description = "Test 1", Image = "blog-feature-img-1.jpg", Date = DateTime.Now },
            new Blog { Id = 2, Title = "Blog2", Description = "Test 2", Image = "blog-feature-img-2.jpg", Date = DateTime.Now },
            new Blog { Id = 3, Title = "Blog3", Description = "Test 3", Image = "blog-feature-img-3.jpg", Date = DateTime.Now });

        }








    }
}
