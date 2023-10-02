using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
