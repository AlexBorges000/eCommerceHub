namespace BlazorShop.Models.DTOs.UsuarioDtos;

public class RequestUpdateSenhaUsuarioDto
{
    public string OldPassword {  get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmedPassword {  get; set; } = string.Empty;
}
