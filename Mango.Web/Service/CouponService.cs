using Mango.Web.Common;
using Mango.Web.Models;
using Mango.Web.Service.Interface;
using Microsoft.Extensions.Options;

namespace Mango.Web.Service;

/// <summary>
/// Representes coupon service to manage coupon data.
/// </summary>
public class CouponService : ICouponService
{
    /// <summary>
    /// Represents interface for base service.
    /// </summary>
    private readonly IBaseService _baseService;

    /// <summary>
    /// Represents the app settings value.
    /// </summary>
    private readonly AppSettings _appSettings;

    public CouponService(IBaseService baseService, IOptions<AppSettings> appSettings)
    {
        _baseService = baseService;
        _appSettings = appSettings.Value;
    }

    public async Task<ResponseDto?> CreateCouponAsync(AddCouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = couponDto,
            Url = _appSettings.CouponAPIUrl + "/api/coupon"
        });
    }

    public async Task<ResponseDto?> DeleteCouponAsync(Guid id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = _appSettings.CouponAPIUrl + $"/api/coupon/{id}"
        });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = _appSettings.CouponAPIUrl + "/api/coupon"
        });
    }

    public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = _appSettings.CouponAPIUrl + $"/api/coupon/GetByCode/{couponCode}"
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(Guid id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = _appSettings.CouponAPIUrl + $"/api/coupon/{id}"
        });
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = couponDto,
            Url = _appSettings.CouponAPIUrl + "/api/coupon"
        });
    }
}
