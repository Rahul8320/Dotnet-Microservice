﻿using Mango.Services.CouponAPI.DB;
using Mango.Services.CouponAPI.Repository.Interface;

namespace Mango.Services.CouponAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    #region repositories

    private ICouponRepository _couponRepository = default!;
    public ICouponRepository CouponRepository
    {
        get
        {
            return _couponRepository ?? new CouponRepository(_context);
        }
    }

    #endregion repositories

    public async Task<bool> Complete() => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _context.Dispose();
    }
}
