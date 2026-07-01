using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IUsuarioService
{
    Task<OperationResult<RequestCadastroUsuarioDto>> InsertUsuario(RequestCadastroUsuarioDto cadastroUsuarioDto);
    Task<OperationResult<RequestUpdateCadastroUsuarioDto>> UpdateUsuario(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuarioDto);
    Task<OperationResult<ResponseGetUsuarioDto>> GetUsuario(string email);
    Task<OperationResult<RequestUpdateSenhaUsuarioDto>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto);
}
