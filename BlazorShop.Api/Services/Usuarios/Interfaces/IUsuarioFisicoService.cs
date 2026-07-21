using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios.Interfaces
{
    public interface IUsuarioFisicoService
    {
        Task<OperationResult<ResponseLoginDto>> InsertUsuarioPfAsync(RequestCadastroUsuarioPfDto requestCadastroUsuariopjDto);
        Task<OperationResult<UsuarioFisico>> UpdateUsuarioPfAsync(int id, RequestUpdateUsuarioPfDto updateCadastroUsuario);
    }
}
