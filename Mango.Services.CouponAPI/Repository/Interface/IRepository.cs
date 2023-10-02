using System.Linq.Expressions;

namespace Mango.Services.CouponAPI.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
