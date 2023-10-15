using Mango.Web.Models;
using Mango.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{ Text = "Admin", Value="Admin"},
            new SelectListItem{Text = "Customer", Value = "Cusmoter"}
        };
        ViewBag.RoleList = roleList;

        return View();
    }

    [HttpGet]
    public IActionResult Logout()
    {
        return View();
    }
}
