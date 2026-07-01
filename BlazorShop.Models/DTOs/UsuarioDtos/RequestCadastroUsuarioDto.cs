
namespace BlazorShop.Models.DTOs.UsuarioDtos;

public class RequestCadastroUsuarioDto
{
    public int TipoPessoa { get; set; }

    public string Email { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha {  get; set; } = string.Empty;
    // PF
    public string? Nome { get; set; }
    public string? CPF { get; set; }

    // PJ
    public string? NomeFantasia { get; set; }
    public string? RazaoSocial { get; set; }
    public string? CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? ResponsavelCompra { get; set; }
}
