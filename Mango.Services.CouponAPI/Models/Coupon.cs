using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public Guid CouponId { get; set; }
        [Required]
        [MinLength(4)]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        [Range(5,90)]
        public double DiscountAmount { get; set; }
        [Range(5,90)]
        public int MinAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}
