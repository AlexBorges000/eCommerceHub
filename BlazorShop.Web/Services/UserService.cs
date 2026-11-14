using BlazorShop.Models.Commons;
using BlazorShop.Models.Config;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Update;
using BlazorShop.Web.Services.Interfaces;
using System.Net;

namespace BlazorShop.Web.Services;

public class UserService : IUserService
{
    public HttpClient _httpClient;
    private readonly ILogger<UserService> _logger;

    public UserService(IHttpClientFactory factory, ILogger<UserService> logger)
    {
        _httpClient = factory.CreateClient(HttpConfiguration.User);
        _logger = logger;
    }

    public async Task<OperationResult<UserPasswordUpdateRequestDto>> ChangePassword(int id, UserPasswordUpdateRequestDto updatePasswordUserDto)
    {

        if (updatePasswordUserDto.NewPassword != updatePasswordUserDto.ConfirmedPassword)
        {
            return OperationResult<UserPasswordUpdateRequestDto>.Fail("As senhas não conhecidem, confira e tente novamente!");
        }
        var response = await _httpClient.GetAsync($"Usuarios/{id}");
        if (!(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent))
        {
            return OperationResult<UserPasswordUpdateRequestDto>.Fail("Usuario não encontrado!");
        }
        var user = await response.Content.ReadFromJsonAsync<OperationResult<UserPasswordUpdateRequestDto>>();
        var updatePassword = await _httpClient.PatchAsJsonAsync($"Usuarios/{id}/senha", updatePasswordUserDto);
        var result = await updatePassword.Content.ReadFromJsonAsync<OperationResult<UserPasswordUpdateRequestDto>>()
                                    ?? OperationResult<UserPasswordUpdateRequestDto>.Fail("Não encontrado");

        return result;
    }

    public async Task<OperationResult<BusinessUserRegistrationRequestDto>> InsertUser(BusinessUserRegistrationRequestDto cadastroUserDto)
    {

        throw new NotImplementedException();
    }

    public Task<OperationResult<RequestUpdateBusinessUserDto>> UpdateUser(int id, RequestUpdateBusinessUserDto updateRegistrationUserDto)
    {
        throw new NotImplementedException();
    }
}
