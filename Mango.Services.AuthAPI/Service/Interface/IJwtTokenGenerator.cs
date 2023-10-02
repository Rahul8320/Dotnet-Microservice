using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Service.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(ApplicationUser applicationUser);
    }
}
