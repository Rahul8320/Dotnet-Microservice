using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dtos;
using Mango.Services.CouponAPI.Repository.Interface;
using Mango.Services.CouponAPI.Service.Interface;

namespace Mango.Services.CouponAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CouponDto> CreateCoupon(AddCouponDto coupon)
        {
            try
            {
                var response = await _unitOfWork.CouponRepository.Find(u => u.CouponCode.Equals(coupon.CouponCode));
                if(response != null)
                {
                    throw new Exception($"Coupon Code: {coupon.CouponCode} is already present!");
                }
                var mapped = _mapper.Map<Coupon>(coupon);
                mapped.CouponId = Guid.NewGuid();
                _unitOfWork.CouponRepository.Add(mapped);
                bool result = await _unitOfWork.Complete();
                if (result)
                {
                    return _mapper.Map<CouponDto>(mapped);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCoupon(Guid couponId)
        {
            try
            {
                var response = await _unitOfWork.CouponRepository.Get(couponId) ?? throw new Exception($"Coupon not found for id: {couponId}");
                _unitOfWork.CouponRepository.Delete(response);
                return await _unitOfWork.Complete();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CouponDto>> GetAllCoupons()
        {
            try
            {
                var response = await _unitOfWork.CouponRepository.GetAll();
                if (response == null) { return Enumerable.Empty<CouponDto>(); }
                return _mapper.Map<IEnumerable<CouponDto>>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            try
            {
                var response = await _unitOfWork.CouponRepository
                    .Find(u => u.CouponCode.Equals(couponCode)) 
                    ?? throw new Exception($"No Coupon found with code: {couponCode}");
                return _mapper.Map<CouponDto>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CouponDto> GetCouponById(Guid couponId)
        {
            try
            {
                var response = await _unitOfWork.CouponRepository.Get(couponId) ?? throw new Exception($"No Coupon found with id: {couponId}");
                return _mapper.Map<CouponDto>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CouponDto> UpdateCoupon(CouponDto coupon)
        {
            try
            {
                var response = await _unitOfWork.CouponRepository.Get(coupon.CouponId) ?? throw new Exception($"No Coupon found with id: {coupon.CouponId}");
                
                response.CouponCode = coupon.CouponCode;
                response.MinAmount = coupon.MinAmount;
                response.DiscountAmount = coupon.DiscountAmount;
                response.UpdatedAt = DateTime.Now;

                _unitOfWork.CouponRepository.Update(response);
                bool result = await _unitOfWork.Complete();
                if (result)
                {
                    return _mapper.Map<CouponDto>(response);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
