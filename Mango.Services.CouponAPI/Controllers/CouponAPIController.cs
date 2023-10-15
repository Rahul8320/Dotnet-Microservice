using Mango.Services.CouponAPI.Models.Dtos;
using Mango.Services.CouponAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
[Authorize]
public class CouponAPIController : ControllerBase
{
    private readonly ICouponService _couponService;
    private readonly ResponseDto _response;
    private readonly ILogger<CouponAPIController> _logger;

    public CouponAPIController(ICouponService couponService, ILogger<CouponAPIController> logger)
    {
        _couponService = couponService;
        _response = new ResponseDto();
        _logger = logger;
    }

    [HttpGet]
    public async Task<ResponseDto> Get()
    {
        try
        {
            IEnumerable<CouponDto> couponList = await _couponService.GetAllCoupons();
            if(couponList == null)
            {
                _logger.LogInformation("No Coupons found!");
                _response.IsSuccess = false;
                _response.Message = "No Coupons Found!";
            }
            else
            {
                _logger.LogInformation("Get Coupon List: {@objList}", couponList);
                _response.Result = couponList;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
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
                _logger.LogInformation($"No Coupon Found with id: {id}");
                _response.Message = $"No Coupon Found with id: {id}";
            }
            _logger.LogInformation("Get Coupon: {@objList}", coupon);
            _response.Result = coupon;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
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
                _logger.LogInformation($"No Coupon Found with code: {code}");
                _response.Message = $"No Coupon Found with code: {code}";
            }
            else
            {
                _logger.LogInformation("Get Coupon: {@objList}", coupon);
                _response.Result = coupon;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Post(AddCouponDto coupon)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCoupon(coupon);
                _logger.LogInformation("Coupon Created {@response}", response);
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
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Put(CouponDto coupon)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.UpdateCoupon(coupon);
                _logger.LogInformation("Coupon Updated {@response}",response);
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
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpDelete]
    [Authorize(Roles = "ADMIN")]
    [Route("{id:Guid}")]
    public async Task<ResponseDto> Delete(Guid id)
    {
        try
        {
            var response = await _couponService.DeleteCoupon(id);

            if(response == true)
            {
                _logger.LogInformation("Coupon with id: {@id} Deleted Successfully!", id);
                _response.Message = $"Coupon with id: {id} Deleted Successfully!";
            }

            _response.IsSuccess = response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
}
