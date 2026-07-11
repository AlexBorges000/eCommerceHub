using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Services.Interfaces;

public interface IUsuarioService
{
    Task<OperationResult<Usuario>> InsertAsync(RequestCadastroUsuarioDto requestCadastroUsuarioDto);
    Task<OperationResult<Usuario>> UpdateAsync(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuario);
    Task<OperationResult<Usuario>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto);
    Task<OperationResult<Usuario>> GetAsync(string email);
    Task<OperationResult<Usuario>> GetByIdAsync(int id);
    Task<OperationResult<Usuario>> DeleteUsuarioAsync(int id);
}
