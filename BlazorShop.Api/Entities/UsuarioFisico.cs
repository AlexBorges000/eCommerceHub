using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class UsuarioFisico : Usuario
{
    [Required]
    [MaxLength(100)]
    public string? Nome { get; set; }
    [Required]
    [MaxLength(250)]
    public string? EncryptCpf { get; set; }
    [Required]
    [MaxLength(250)]
    public string? HashCpf { get; set; }
}
