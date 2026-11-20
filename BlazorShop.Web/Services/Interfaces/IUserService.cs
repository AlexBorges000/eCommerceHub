using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Update;

namespace BlazorShop.Web.Services.Interfaces;

public interface IUserService
{
    Task<OperationResult<BusinessUserRegistrationRequestDto>> InsertUser(BusinessUserRegistrationRequestDto cadastroUserDto);
    Task<OperationResult<RequestUpdateBusinessUserDto>> UpdateUser(int id, RequestUpdateBusinessUserDto updateRegistrationUserDto);
    Task<OperationResult<UserPasswordUpdateRequestDto>> ChangePassword(int id, UserPasswordUpdateRequestDto updatePasswordUserDto);
}
