using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
namespace BlazorShop.Api.Mappings.Usuarios;

public static class UsuarioMappers
{
    public static UsuarioJuridico ToEntity(this RequestCadastroUsuarioPjDto dto)
    {
        return new UsuarioJuridico
        {
            Email = dto.Email,
            Telefone = dto.Telefone,
            NomeFantasia = dto.NomeFantasia,
            RazaoSocial = dto.RazaoSocial,
            InscricaoEstadual = dto.InscricaoEstadual,
            ResponsavelCompra = dto.ResponsavelCompra
        };
    }
    public static UsuarioFisico ToEntity(this RequestCadastroUsuarioPfDto dto)
    {
        return new UsuarioFisico
        {
            Email = dto.Email,
            Telefone = dto.Telefone,
            Nome = dto.Nome,
        };
    }

}
