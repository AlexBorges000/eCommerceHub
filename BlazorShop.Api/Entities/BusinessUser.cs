using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class BusinessUser : User
{
    [Required]
    [MaxLength(100)]
    public string TradeName { get; set; } = string.Empty;
    [Required]
    [MaxLength(150)]
    public string LegalName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string PurchasingContact { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? StateRegistration { get; set; }
    [Required]
    [MaxLength(250)]
    public string HashBusinessTaxId { get; set; } = string.Empty;
    [Required]
    [MaxLength(250)]
    public string EncryptBusinessTaxId { get; set; } = string.Empty;

}
