using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IUsuarioService
{
    Task<OperationResult<CadastroUsuarioDto>> InsertUsuario(CadastroUsuarioDto cadastroUsuarioDto);
    Task<OperationResult<UpdateCadastroUsuarioDto>> UpdateUsuario(int id, UpdateCadastroUsuarioDto updateCadastroUsuarioDto);
    Task<OperationResult<ResponseGetUsuarioDto>> GetUsuario(int id);
    Task<OperationResult<UpdateSenhaUsuarioDto>> ChangePassword(int id, UpdateSenhaUsuarioDto updateSenhaUsuarioDto);
}
