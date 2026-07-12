using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorShop.Models.DTOs.UsuarioDtos.Login;

public class ResponseGetUsuarioDto
{
    public string Email { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public string? NomeFantasia { get; set; }
    public string? RazaoSocial { get; set; }
    public string? ResponsavelCompra { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? CNPJ { get; set; }

}
