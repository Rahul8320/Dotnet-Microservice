using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models.Dtos
{
    public class UserDto
    {
        [Required]
        public string ID { get; set; } = String.Empty;

        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
