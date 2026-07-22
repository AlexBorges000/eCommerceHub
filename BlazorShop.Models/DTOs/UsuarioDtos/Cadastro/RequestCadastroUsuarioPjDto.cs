namespace BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;

public class RequestCadastroUsuarioPjDto
{
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha {  get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string InscricaoEstadual { get; set; } = string.Empty;
    public string ResponsavelCompra { get; set; } = string.Empty;
}
