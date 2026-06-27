using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.UsuarioDtos;
using BlazorShop.Web.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BlazorShop.Web.Services;

public class UsuarioService : IUsuarioService
{
    public HttpClient _httpClient;
    private readonly ILogger<ProdutoService> _logger;

    public UsuarioService(IHttpClientFactory factory, ILogger<ProdutoService> logger)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Usuario);
        _logger = logger;
    }

    public async Task<OperationResult<UpdateSenhaUsuarioDto>> ChangePassword(int id, UpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        //TODO: fazer o Hash da senha, tambem fazer a criptografia de dados sensiveis
        if (updateSenhaUsuarioDto.NewPassword != updateSenhaUsuarioDto.ConfirmedPassword)
        {
            return OperationResult<UpdateSenhaUsuarioDto>.Fail("As senhas não conhecidem, confira e tente novamente!");
        }
        var response = await _httpClient.GetAsync($"Usuarios/{id}");
        if (!(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent))
        {
            return OperationResult<UpdateSenhaUsuarioDto>.Fail("Falha ao encontrar usuario!");
        }
        var usuario = await response.Content.ReadFromJsonAsync<OperationResult<UpdateSenhaUsuarioDto>>();
        if (usuario.Value.OldPassword != updateSenhaUsuarioDto.OldPassword)
        {
            return OperationResult<UpdateSenhaUsuarioDto>.Fail("Erro ao verificar a senha atual!");
        }

        var updatePassword = await _httpClient.PatchAsJsonAsync($"Usuarios/{id}/senha", updateSenhaUsuarioDto);
        var result = await updatePassword.Content.ReadFromJsonAsync<OperationResult<UpdateSenhaUsuarioDto>>();
        return OperationResult<UpdateSenhaUsuarioDto>.Ok(result.Value);

    }

    public Task<OperationResult<ResponseGetUsuarioDto>> GetUsuario(string id)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<ResponseGetUsuarioDto>> GetUsuario(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<CadastroUsuarioDto>> InsertUsuario(CadastroUsuarioDto cadastroUsuarioDto)
    {

        throw new NotImplementedException();
    }

    public Task<OperationResult<UpdateCadastroUsuarioDto>> UpdateUsuario(int id, UpdateCadastroUsuarioDto updateCadastroUsuarioDto)
    {
        throw new NotImplementedException();
    }
    public string HashPassword(string password, string confirmPassword)
    {
        throw new NotImplementedException();
    }
}
