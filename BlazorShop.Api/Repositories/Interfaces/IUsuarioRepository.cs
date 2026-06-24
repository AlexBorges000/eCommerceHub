using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{   
    Task<OperationResult<Usuario>> InsertUsuario(CadastroUsuarioDto cadastroUsuario);
    Task<OperationResult<Usuario>> UpdateUsuario(int id, UpdateCadastroUsuarioDto updateCadastroUsuario);
    Task<OperationResult<Usuario>> GetUsuario(int id);

}
