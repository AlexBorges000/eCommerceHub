namespace BlazorShop.Models.DTOs.TokensDto;

public class ResponseLoginDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
