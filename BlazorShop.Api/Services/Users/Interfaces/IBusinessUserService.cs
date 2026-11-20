using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Update;

namespace BlazorShop.Api.Services.Users.Interfaces;

public interface IBusinessUserService
{
    Task<OperationResult<ResponseLoginDto>> InsertBusinessUserAsync(BusinessUserRegistrationRequestDto requestRegistrationUserDto);
    Task<OperationResult<BusinessUser>> UpdateBusinessUserAsync(int id, RequestUpdateBusinessUserDto updateRegistrationBusinessUser);
}
