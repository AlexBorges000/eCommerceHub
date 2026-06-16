using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{
    public Task<Usuario> InsertUsurio(CadastroUsuarioDto cadastroUsuario);
    public Task<Usuario> UpdateUsuario(UpdateCadastroUsuarioDto updateCadastroUsuario);
    public Task<Usuario> GetUsuario(int id);

}
