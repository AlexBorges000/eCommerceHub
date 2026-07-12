using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class UsuarioJuridico : Usuario
{
    [Required]
    [MaxLength(100)]
    public string NomeFantasia { get; set; } = string.Empty;
    [Required]
    [MaxLength(150)]
    public string RazaoSocial { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string ResponsavelCompra { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? InscricaoEstadual { get; set; }
    [Required]
    [MaxLength(250)]
    public string HashCNPJ { get; set; } = string.Empty;
    [Required]
    [MaxLength(250)]
    public string EncryptCNPJ { get; set; } = string.Empty;

}
