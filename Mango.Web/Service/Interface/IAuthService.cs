using Mango.Web.Models;

namespace Mango.Web.Service.Interface;

/// <summary>
/// Represents the auth service intreface to manage user data. 
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// User login service.
    /// </summary>
    /// <param name="loginRequestDto">Login request information.</param>
    /// <returns>Response dto if login success or null</returns>
    Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

    /// <summary>
    /// User registration service.
    /// </summary>
    /// <param name="registrationRequestDto">Registre request information.</param>
    /// <returns>Response dto if register success or null.</returns>
    Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);

    /// <summary>
    /// User role assign service
    /// </summary>
    /// <param name="registrationRequestDto">User role assign request information.</param>
    /// <returns>Response dto if assign role is success or null.</returns>
    Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
}
