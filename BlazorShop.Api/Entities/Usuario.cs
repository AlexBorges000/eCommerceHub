using BlazorShop.Api.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public class Usuario
{
    public int Id { get; set; }
    public TipoPessoa TipoPessoa { get; set; }

    // Comuns
    [Required]
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Endereco { get; set; } = string.Empty;
    [Required]
    [MaxLength(20)]
    public string Telefone { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    public string Senha { get; set; } = string.Empty;

    // PF
    [MaxLength(100)]
    public string? Nome { get; set; }
    [MaxLength(250)]
    public string? CPF { get; set; }

    // PJ
    [MaxLength(100)]
    public string? NomeFantasia { get; set; }
    [MaxLength(150)]
    public string? RazaoSocial { get; set; }
    [MaxLength(100)]
    public string? ResponsavelCompra { get; set; }
    [MaxLength(20)]
    public string? InscricaoEstadual { get; set; }
    [MaxLength(250)]
    public string? CNPJ { get; set; }
    public Carrinho Carrinho { get; set; } = null!;
    public ICollection<RefreshTokens> RefreshTokens { get; set; } = [];
    public Role Role { get; set; } = null!;
    public int RoleId { get; set; }
}