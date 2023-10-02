using Mango.Services.AuthAPI.Models.Dtos;

namespace Mango.Services.AuthAPI.Service.Interface
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
