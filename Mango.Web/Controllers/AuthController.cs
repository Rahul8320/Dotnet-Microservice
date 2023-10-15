using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Mango.Web.Common;
using Mango.Web.Models;
using Mango.Web.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;

    public AuthController(IAuthService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public IActionResult Login()
    {
        var loginRequestDto = new LoginRequestDto();
        return View(loginRequestDto);
    }

     [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);

        if(response != null && response.IsSuccess)
        {
           var result = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result)!);

            if(result is not null && result.Token != null)
            {
                await SignInUser(result);
                _tokenProvider.SetToken(result.Token);
            }
           return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["error"] = response?.Message ?? "Something gone wrong!";
            ModelState.AddModelError("CustomError", response?.Message ?? "Something gone wrong!");
            return View(request);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{ Text = RoleTypes.Admin.ToString(), Value=RoleTypes.Admin.ToString() },
            new SelectListItem{ Text = RoleTypes.Customer.ToString(), Value=RoleTypes.Customer.ToString() }
        };
        ViewBag.RoleList = roleList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto request)
    {
        ResponseDto? result = await _authService.RegisterAsync(request);
        ResponseDto? response;

        if(result != null && result.IsSuccess)
        {
            if(string.IsNullOrEmpty(request.Role.ToString()))
            {
                request.Role = RoleTypes.Customer;
            }
            
            response = await _authService.AssignRoleAsync(request);

            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Registration Successful.";
                return RedirectToAction(nameof(Login));
            }
        }
        TempData["error"] = result?.Message ?? "Something gone wrong!";
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{ Text = RoleTypes.Admin.ToString(), Value=RoleTypes.Admin.ToString() },
            new SelectListItem{ Text = RoleTypes.Customer.ToString(), Value=RoleTypes.Customer.ToString() }
        };
        ViewBag.RoleList = roleList;

        return View(request);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        TempData["error"] = "You don't have any access to view this page!";
       return RedirectToAction("Index", "Home");
    }

    private async Task SignInUser(LoginResponseDto model)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(model.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.FamilyName, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.FamilyName)!.Value));

        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.FamilyName)!.Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role")!.Value));

        var principle = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
    }
}
