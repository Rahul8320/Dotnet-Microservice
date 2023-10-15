using System.ComponentModel.DataAnnotations;
using Mango.Web.Common;

namespace Mango.Web.Models;

public class RegistrationRequestDto
{
    [Required(ErrorMessage = "You must have an email!")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must have a name!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must have a phone number!")]
    [MinLength(10, ErrorMessage = "Your phone number must be 10 digit.")]
    [MaxLength(10, ErrorMessage = "Your phone number must be 10 digit.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must have a password")]
    [MinLength(8, ErrorMessage = "Your password must be 8 character.")]
    [MaxLength(16, ErrorMessage = "Your password must not be more than 16 character.")]
    public string Password { get; set; } = string.Empty;

    public RoleTypes? Role { get; set; }
}
