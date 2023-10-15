using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models.Dtos;

public class CouponDto
{
    [Required(ErrorMessage = "Coupon Id is Required")]
    public Guid CouponId { get; set; }

    [Required(ErrorMessage = "Coupon Code is Required")]
    [MinLength(4, ErrorMessage = "Coupon Code Must be 4 characters")]
    public string CouponCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Discount Amount is Required")]
    [Range(5, 90, ErrorMessage = "Value must be between 5 and 90")]
    public double DiscountAmount { get; set; }

    [Required(ErrorMessage = "Min Amount is Required")]
    [Range(5, 90, ErrorMessage = "Value must be between 5 and 90")]
    public int MinAmount { get; set; }
}
