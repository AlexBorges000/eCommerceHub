using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Services.Users.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Update;

namespace BlazorShop.Api.Services.Users;

public class UserService(IPasswordService passwordService,
                            IUserRepository userRepository) : IUserService
{
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<OperationResult<User>> ChangePassword(int id, UserPasswordUpdateRequestDto updatePasswordUserDto)
    {
        if (updatePasswordUserDto.ConfirmedPassword != updatePasswordUserDto.NewPassword)
        {
            return OperationResult<User>.Fail("As senhas não coincidem");
        }

        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return OperationResult<User>.Fail("Usuario Não encontrado");
        }
        if (_passwordService.IsInvalidPassword(user,
            hashedPassword: user.Password,
            password: updatePasswordUserDto.OldPassword))
        {
            return OperationResult<User>.Fail("A senha antiga esta errada");
        }
        var hashedPassword = _passwordService.HashedPassword(user, updatePasswordUserDto.NewPassword);
        user.Password = hashedPassword;
        await _userRepository.UpdateAsync(user);
        return OperationResult<User>.Ok(user);
    }

    public async Task<OperationResult<User>> GetByEmailAsync(string email)
    {

        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
        {
            return OperationResult<User>.Fail("Senha ou Email Invalidos");
        }
        return OperationResult<User>.Ok(user);
    }

    public async Task<OperationResult<User>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return OperationResult<User>.Fail("Usuario não Cadastrado");
        }
        return OperationResult<User>.Ok(user);
    }

    public async Task<OperationResult<User>> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return OperationResult<User>.Fail("Usuario Não encontrado para deletar");
        }
        await _userRepository.DeleteUserAsync(user);
        return OperationResult<User>.Ok(user, message: "Usuario deletado com sucesso");

    }
}
