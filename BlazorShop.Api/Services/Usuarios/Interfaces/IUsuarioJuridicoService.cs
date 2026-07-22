using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios.Interfaces;

public interface IUsuarioJuridicoService
{
    Task<OperationResult<ResponseLoginDto>> InsertUsuarioPjAsync(RequestCadastroUsuarioPjDto requestCadastroUsuarioDto);
    Task<OperationResult<UsuarioJuridico>> UpdateUsuarioPjAsync(int id, RequestUpdateUsuarioPjDto updateCadastroPjUsuario);
}
