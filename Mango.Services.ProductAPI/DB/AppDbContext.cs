using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DB;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    // Seed Data to Database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = Guid.NewGuid(),
            Name = "Samosa",
            Price = 15,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://www.zedamagazine.com/wp-content/uploads/2018/06/Indian-Food-Samosa-Dish-HD-Wallpapers.jpg",
            CategoryName = "Appetizer"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = Guid.NewGuid(),
            Name = "Paneer Tikka",
            Price = 13.99,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "http://www.cookforindia.com/wp-content/uploads/2016/08/Paneer-Tikka-_LR.jpg",
            CategoryName = "Appetizer"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = Guid.NewGuid(),
            Name = "Sweet Pie",
            Price = 10.99,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://img.sndimg.com/food/image/upload/v1/img/recipes/30/76/6/pic0L9jII.jpg",
            CategoryName = "Dessert"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = Guid.NewGuid(),
            Name = "Pav Bhaji",
            Price = 15,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://i.pinimg.com/originals/2d/37/a6/2d37a6a69d254a42ca1a345304120796.jpg",
            CategoryName = "Entree"
        });
    }
}
