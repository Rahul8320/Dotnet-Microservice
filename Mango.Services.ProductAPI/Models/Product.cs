using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models;

public class Product
{
    [Key]
    public Guid ProductId { get; set; }
    [Required]
    [MinLength(4)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(1, 5000)]
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
