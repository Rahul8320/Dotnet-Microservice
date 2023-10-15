using Mango.Services.ProductAPI.DB;
using Mango.Services.ProductAPI.Repository.Interfaces;

namespace Mango.Services.ProductAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    #region repositories

    private IProductRepository _productRepository = default!;

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepository ?? new ProductRepository(_context);
        }
    }

    #endregion repositories

    public async Task<bool> Complete() => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _context.Dispose();
    }
}
