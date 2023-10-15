using Mango.Services.CouponAPI.Models.Dtos;

namespace Mango.Services.CouponAPI.Service.Interface;

public interface ICouponService
{
    Task<IEnumerable<CouponDto>> GetAllCoupons();
    Task<CouponDto> GetCouponById(Guid couponId);
    Task<CouponDto> GetCouponByCode(string couponCode);
    Task<CouponDto> CreateCoupon(AddCouponDto coupon);
    Task<CouponDto> UpdateCoupon(CouponDto coupon);
    Task<bool> DeleteCoupon(Guid couponId);
}
