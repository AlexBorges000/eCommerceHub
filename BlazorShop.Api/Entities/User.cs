
using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Api.Entities;

public abstract class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = null!;
    public int RoleId { get; set; }
    public ICollection<RefreshTokens> RefreshTokens { get; set; } = [];
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public Cart Cart { get; set; } = null!;
}