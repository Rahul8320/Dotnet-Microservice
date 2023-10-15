using Mango.Services.CouponAPI.DB;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Repository.Interface;

namespace Mango.Services.CouponAPI.Repository;

internal class CouponRepository : Repository<Coupon>, ICouponRepository
{
    private readonly AppDbContext _context;

    public CouponRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}