using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class IndividualUser : User
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(250)]
    public string? EncryptCpf { get; set; }
    [Required]
    [MaxLength(250)]
    public string? HashCpf { get; set; }
}
