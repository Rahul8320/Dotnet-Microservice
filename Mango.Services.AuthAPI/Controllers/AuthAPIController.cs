using Mango.Services.AuthAPI.Models.Dtos;
using Mango.Services.AuthAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var errorMessage = await _authService.Register(model);
                    if(!string.IsNullOrEmpty(errorMessage))
                    {
                        _response.IsSuccess = false;
                        _response.Message = errorMessage;
                        return BadRequest(_response);
                    }
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.Message = "Validation Failed";
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
