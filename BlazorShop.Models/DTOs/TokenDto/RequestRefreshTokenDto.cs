namespace BlazorShop.Models.DTOs.TokenDto;

public class RequestRefreshTokenDto
{
    public string RefreshToken { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public DateTime DataExpiracao { get; set; }
}
