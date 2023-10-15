using Mango.Web.Common;
using Mango.Web.Service.Interface;
using Microsoft.Extensions.Options;

namespace Mango.Web.Service;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppSettings _appSettings;

    public TokenProvider(IHttpContextAccessor contextAccessor, IOptions<AppSettings> appSettings)
    {
        _contextAccessor = contextAccessor;
        _appSettings = appSettings.Value;
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(_appSettings.TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(_appSettings.TokenCookie, out token);
        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(_appSettings.TokenCookie, token);
    }
}
