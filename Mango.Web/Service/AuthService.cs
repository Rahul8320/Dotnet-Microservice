using Mango.Web.Common;
using Mango.Web.Models;
using Mango.Web.Service.Interface;
using Microsoft.Extensions.Options;

namespace Mango.Web.Service;

public class AuthService : IAuthService
{
    /// <summary>
    /// Represents interface for base service.
    /// </summary>
    private readonly IBaseService _baseService;

    /// <summary>
    /// Represents the app settings value.
    /// </summary>
    private readonly AppSettings _appSettings;

    public AuthService(IBaseService baseService, IOptions<AppSettings> appSettings)
    {
        _baseService = baseService;
        _appSettings = appSettings.Value;
    }

    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = registrationRequestDto,
            Url = _appSettings.AuthAPIUrl + "/api/auth/assign-role"
        });
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = loginRequestDto,
            Url = _appSettings.AuthAPIUrl + "/api/auth/login"
        }, withBearer: false);
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = registrationRequestDto,
            Url = _appSettings.AuthAPIUrl + "/api/auth/register"
        }, withBearer: false);
    }
}
