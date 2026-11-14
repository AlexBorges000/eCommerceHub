namespace BlazorShop.Models.DTOs.UserDtos.Registration;

public class ResponseLoginDto
{
    public string Token { get; set; } =string.Empty;
    public string RefreshToken { get; set; } =string.Empty;
}
