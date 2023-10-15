using Mango.Web.Models;
using Mango.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
            new SelectListItem{ Text = "Admin", Value="Admin"},
            new SelectListItem{Text = "Customer", Value = "Customer"}
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
            if(string.IsNullOrEmpty(request.Role))
            {
                request.Role = "Customer";
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
            new SelectListItem{ Text = "Admin", Value="Admin"},
            new SelectListItem{Text = "Customer", Value = "Customer"}
        };
        ViewBag.RoleList = roleList;

        return View(request);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        return View();
    }
}
