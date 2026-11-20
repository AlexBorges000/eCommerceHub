namespace BlazorShop.Models.DTOs.UserDtos.Registration;

public class BusinessUserRegistrationRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword {  get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string? StateRegistration { get; set; }
    public string PurchasingContact { get; set; } = string.Empty;
}
