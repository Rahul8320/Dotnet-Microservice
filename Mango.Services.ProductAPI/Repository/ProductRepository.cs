using Mango.Services.ProductAPI.DB;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Repository.Interfaces;

namespace Mango.Services.ProductAPI.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
