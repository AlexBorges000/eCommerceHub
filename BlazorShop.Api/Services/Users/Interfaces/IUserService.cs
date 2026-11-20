using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Update;

namespace BlazorShop.Api.Services.Users.Interfaces;

public interface IUserService
{
    Task<OperationResult<User>> ChangePassword(int id, UserPasswordUpdateRequestDto updatePasswordUserDto);
    Task<OperationResult<User>> GetByEmailAsync(string email);
    Task<OperationResult<User>> GetByIdAsync(int id);
    Task<OperationResult<User>> DeleteUserAsync(int id);
}
