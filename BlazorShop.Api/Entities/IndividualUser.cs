using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class IndividualUser : User
{
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(250)]
    public string? EncryptTaxId { get; set; }
    [Required]
    [MaxLength(250)]
    public string? HashTaxId { get; set; }
}
