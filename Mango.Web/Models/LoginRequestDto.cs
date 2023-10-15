using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models;

public class LoginRequestDto
{
    [Required(ErrorMessage = "You must have an email!")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "You must have a password")]
    public string Password { get; set; } = string.Empty;
}
