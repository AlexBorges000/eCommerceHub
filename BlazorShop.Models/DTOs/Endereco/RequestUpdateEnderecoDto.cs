namespace BlazorShop.Models.DTOs.Endereco;

public class RequestUpdateEnderecoDto
{
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Cidade { get; set; }
    public string? Bairro { get; set; }
    public string? CEP { get; set; }
    public bool? Principal { get; set; }
}
