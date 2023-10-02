using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class RequestDto
    {
        [Required]
        public ApiType ApiType { get; set; } = ApiType.GET;

        [Required]
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string? AccessToken { get; set; }

    }
}
