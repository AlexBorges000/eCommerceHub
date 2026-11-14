using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models.DTOs.UserDtos.Registration;

public class IndividualUserRegistrationRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmaPassword { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;

}
