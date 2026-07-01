using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.UsuarioDtos;
using BlazorShop.Web.Services.Interfaces;
using System.Net;

namespace BlazorShop.Web.Services;

public class UsuarioService : IUsuarioService
{
    public HttpClient _httpClient;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(IHttpClientFactory factory, ILogger<UsuarioService> logger)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.Usuario);
        _logger = logger;
    }

    public async Task<OperationResult<RequestUpdateSenhaUsuarioDto>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {

        if (updateSenhaUsuarioDto.NewPassword != updateSenhaUsuarioDto.ConfirmedPassword)
        {
            return OperationResult<RequestUpdateSenhaUsuarioDto>.Fail("As senhas não conhecidem, confira e tente novamente!");
        }
        var response = await _httpClient.GetAsync($"Usuarios/{id}");
        if (!(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent))
        {
            return OperationResult<RequestUpdateSenhaUsuarioDto>.Fail("Usuario não encontrado!");
        }
        var usuario = await response.Content.ReadFromJsonAsync<OperationResult<RequestUpdateSenhaUsuarioDto>>();
        var updatePassword = await _httpClient.PatchAsJsonAsync($"Usuarios/{id}/senha", updateSenhaUsuarioDto);
        var result = await updatePassword.Content.ReadFromJsonAsync<OperationResult<RequestUpdateSenhaUsuarioDto>>()
                                    ?? OperationResult<RequestUpdateSenhaUsuarioDto>.Fail("Não encontrado");

        return result;
    }

    public async Task<OperationResult<ResponseGetUsuarioDto>> GetUsuario(string email)
    {
        var response = await _httpClient.GetAsync($"Usuarios?email={Uri.EscapeDataString(email)}");
        if (response.IsSuccessStatusCode && !(response.StatusCode == HttpStatusCode.NoContent))
        {
            var usuario = await response.Content.ReadFromJsonAsync<ResponseGetUsuarioDto>();
            if (usuario is not null)
                return OperationResult<ResponseGetUsuarioDto>.Ok(usuario);
            return OperationResult<ResponseGetUsuarioDto>.Fail("Usuario não cadastrado");
        }
        return OperationResult<ResponseGetUsuarioDto>.Fail("Usuario não cadastrado");
    }

    public async Task<OperationResult<RequestCadastroUsuarioDto>> InsertUsuario(RequestCadastroUsuarioDto cadastroUsuarioDto)
    {

        throw new NotImplementedException();
    }

    public Task<OperationResult<RequestUpdateCadastroUsuarioDto>> UpdateUsuario(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuarioDto)
    {
        throw new NotImplementedException();
    }
}
