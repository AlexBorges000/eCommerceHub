using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorShop.Models.DTOs.UsuarioDtos;

public class UpdateCadastroUsuarioDto
{
    public string? NomeFantasia { get; set; }
    public string? RazaoSocial { get; set; }
    public string? ResponsavelCompra { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? Endereco { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }

}
