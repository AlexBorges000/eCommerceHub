using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios.Interfaces;

public interface IUsuarioService
{
    Task<OperationResult<Usuario>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto);
    Task<OperationResult<Usuario>> GetByEmailAsync(string email);
    Task<OperationResult<Usuario>> GetByIdAsync(int id);
    Task<OperationResult<Usuario>> DeleteUsuarioAsync(int id);
}
