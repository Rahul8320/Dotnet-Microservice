using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models;

public class UserDto
{
    [Required]
    public string ID { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
}
