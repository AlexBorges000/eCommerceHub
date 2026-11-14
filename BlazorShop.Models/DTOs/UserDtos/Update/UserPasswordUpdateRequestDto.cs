namespace BlazorShop.Models.DTOs.UserDtos.Update;

public class UserPasswordUpdateRequestDto
{
    public string OldPassword {  get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmedPassword {  get; set; } = string.Empty;
}
