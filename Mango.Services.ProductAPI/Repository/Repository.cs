using Mango.Services.ProductAPI.DB;
using Mango.Services.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mango.Services.ProductAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        if (entity == null) throw new Exception("Entity is Null");

        _context.Set<T>().Add(entity);

    }

    public void Update(T entity)
    {
        if (entity == null) throw new Exception("Entity is Null");

        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        if (entity == null) throw new Exception("Entity is Null");

        _context.Set<T>().Remove(entity);
    }

    public async Task<T> Find(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);

    public async Task<T> Get(Guid id) => await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
}
