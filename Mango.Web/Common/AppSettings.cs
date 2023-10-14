namespace Mango.Web.Common;

/// <summary>
/// Represent static details like endpoint urls
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Gets or Sets Coupon api base url.
    /// </summary>
    public string CouponAPIUrl { get; set; } = String.Empty;

    /// <summary>
    /// Gets or Sets auth api base url.
    /// </summary>
    public string AuthAPIUrl { get; set; } = String.Empty;
}
