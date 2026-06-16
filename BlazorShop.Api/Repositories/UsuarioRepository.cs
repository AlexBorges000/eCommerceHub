using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    public Task<Usuario> GetUsuario(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> InsertUsurio(CadastroUsuarioDto cadastroUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> UpdateUsuario(UpdateCadastroUsuarioDto updateCadastroUsuario)
    {
        throw new NotImplementedException();
    }
}
