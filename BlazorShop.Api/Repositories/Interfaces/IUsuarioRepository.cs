using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{   
    Task<OperationResult<Usuario>> AddAsync(RequestCadastroUsuarioDto cadastroUsuario);
    Task<OperationResult<Usuario>> UpdateAsync(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuario);
    Task<Usuario?> GetAsync(string email);
    Task<OperationResult<Usuario>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto);
    Task<Usuario?> GetAsync(int id);
}
