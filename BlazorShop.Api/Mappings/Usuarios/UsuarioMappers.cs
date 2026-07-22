using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;
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
    public static void UpdateEntity(this RequestUpdateUsuarioPfDto dto, UsuarioFisico entity)
    {
        if (dto.Telefone != null)
            entity.Telefone = dto.Telefone;
    }

    public static void UpdateEntity(this RequestUpdateUsuarioPjDto dto, UsuarioJuridico entity)
    {
        if (dto.Telefone != null)
            entity.Telefone = dto.Telefone;

        if (dto.RazaoSocial != null)
            entity.RazaoSocial = dto.RazaoSocial;

        if (dto.ResponsavelCompra != null)
            entity.ResponsavelCompra = dto.ResponsavelCompra;

        if (dto.NomeFantasia != null)
            entity.NomeFantasia = dto.NomeFantasia;
    }

}
