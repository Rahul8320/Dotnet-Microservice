using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models.Dtos
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        public string? Role {get; set;}
    }
}
