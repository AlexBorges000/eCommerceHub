
using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public abstract class Usuario
{
    public int Id { get; set; }
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
    public Carrinho Carrinho { get; set; } = null!;
    public ICollection<RefreshTokens> RefreshTokens { get; set; } = [];
    public Role Role { get; set; } = null!;
    public int RoleId { get; set; }

}