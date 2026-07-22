using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Web.Services.Interfaces;

public interface IUsuarioService
{
    Task<OperationResult<RequestCadastroUsuarioPjDto>> InsertUsuario(RequestCadastroUsuarioPjDto cadastroUsuarioDto);
    Task<OperationResult<RequestUpdateUsuarioPjDto>> UpdateUsuario(int id, RequestUpdateUsuarioPjDto updateCadastroUsuarioDto);
    Task<OperationResult<RequestUpdateSenhaUsuarioDto>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto);
}
