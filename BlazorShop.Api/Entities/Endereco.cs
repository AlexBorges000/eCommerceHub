using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class Endereco
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    [Required]
    [MaxLength(250)]
    public string Logradouro { get; set; } = string.Empty;
    [Required]
    [MaxLength(10)]
    public string Numero { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Complemento { get; set; }
    [Required]
    [MaxLength(100)]
    public string Cidade { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Bairro { get; set; } = string.Empty;
    [Required]
    [MaxLength(2)]
    public string UF { get; set; } = string.Empty;
    [Required]
    [MaxLength(10)]
    public string CEP { get; set; } = string.Empty;
    public bool Principal { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
