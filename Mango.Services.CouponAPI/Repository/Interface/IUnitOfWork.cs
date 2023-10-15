namespace Mango.Services.CouponAPI.Repository.Interface;

public interface IUnitOfWork : IDisposable
{
    ICouponRepository CouponRepository { get; }
    Task<bool> Complete();
}
