using Mango.Web.Models;

namespace Mango.Web.Service.Interface
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(Guid id);
        Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);
        Task<ResponseDto?> CreateCouponAsync(AddCouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(Guid id);
    }
}
