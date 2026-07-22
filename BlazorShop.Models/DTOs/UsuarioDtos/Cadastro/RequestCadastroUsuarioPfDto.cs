using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;

public class RequestCadastroUsuarioPfDto
{
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;

}
