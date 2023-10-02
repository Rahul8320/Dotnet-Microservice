using Mango.Web.Models;
using Mango.Web.Service.Interface;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public Task<ResponseDto?> CreateCouponAsync(AddCouponDto couponDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteCouponAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetCouponByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            throw new NotImplementedException();
        }
    }
}
