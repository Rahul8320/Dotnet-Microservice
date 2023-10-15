namespace Mango.Services.ProductAPI.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    Task<bool> Complete();
}
