namespace BlazorShop.Models.DTOs.UsuarioDtos;

public class RequestLoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
