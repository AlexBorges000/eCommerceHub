using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios.Interfaces
{
    public interface IUsuarioFisicoService
    {
        Task<OperationResult<UsuarioFisico>> InsertUsuarioPfAsync(RequestCadastroUsuarioPfDto requestCadastroUsuariopjDto);
        Task<OperationResult<UsuarioFisico>> UpdateUsuarioPfAsync(int id, RequestUpdateUsuarioPfDto updateCadastroUsuario);
    }
}
