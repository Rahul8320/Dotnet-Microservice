using Mango.Services.CouponAPI.Models.Dtos;
using Mango.Services.CouponAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly ResponseDto _response;

        public CouponAPIController(ICouponService couponService)
        {
            _couponService = couponService;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<CouponDto> couponList = await _couponService.GetAllCoupons();
                if(couponList == null)
                {
                    Log.Information("No Coupons found!");
                    _response.IsSuccess = false;
                    _response.Message = "No Coupons Found!";
                }
                else
                {
                    Log.Information("Get Coupon List: {@objList}", couponList);
                    _response.Result = couponList;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ResponseDto> Get(Guid id) {
            try
            {
                CouponDto coupon = await _couponService.GetCouponById(id);
                if(coupon == null)
                {
                    _response.IsSuccess = false;
                    Log.Information($"No Coupon Found with id: {id}");
                    _response.Message = $"No Coupon Found with id: {id}";
                }
                Log.Information("Get Coupon: {@objList}", coupon);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<ResponseDto> GetByCode(string code)
        {
            try
            {
                if(code.Trim().Length <= 3)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon Code is Invalid!";
                    return _response;   
                }

                CouponDto coupon = await _couponService.GetCouponByCode(code);
                if(coupon == null)
                {
                    _response.IsSuccess = false;
                    Log.Information($"No Coupon Found with code: {code}");
                    _response.Message = $"No Coupon Found with code: {code}";
                }
                else
                {
                    Log.Information("Get Coupon: {@objList}", coupon);
                    _response.Result = coupon;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post(AddCouponDto coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _couponService.CreateCoupon(coupon);
                    Log.Information($"Coupon Created {response}");
                    _response.Result = response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Result = ModelState;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put(CouponDto coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _couponService.UpdateCoupon(coupon);
                    Log.Information($"Coupon Updated {response}");
                    _response.Result = response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Result = ModelState;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ResponseDto> Delete(Guid id)
        {
            try
            {
                var response = await _couponService.DeleteCoupon(id);
                if(response == true)
                {
                    _response.Message = $"Coupon with id: {id} Deleted Successfully!";
                }
                _response.IsSuccess = response;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
