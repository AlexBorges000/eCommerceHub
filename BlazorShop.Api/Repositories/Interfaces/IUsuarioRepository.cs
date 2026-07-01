using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{   
    Task<OperationResult<Usuario>> InsertUsuario(RequestCadastroUsuarioDto cadastroUsuario);
    Task<OperationResult<Usuario>> UpdateUsuario(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuario);
    Task<Usuario?> GetUsuario(string email);
    Task<OperationResult<Usuario>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto);
}
