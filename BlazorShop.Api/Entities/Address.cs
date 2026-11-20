using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [Required]
    [MaxLength(250)]
    public string Street { get; set; } = string.Empty;
    [Required]
    [MaxLength(10)]
    public string Number { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Complement { get; set; }
    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string District { get; set; } = string.Empty;
    [Required]
    [MaxLength(2)]
    public string UF { get; set; } = string.Empty;
    [Required]
    [MaxLength(10)]
    public string PostalCode { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public User User { get; set; } = null!;
}
