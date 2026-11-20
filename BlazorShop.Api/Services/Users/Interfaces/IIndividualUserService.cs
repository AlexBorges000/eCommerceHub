using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Login;
using BlazorShop.Models.DTOs.UserDtos.Update;

namespace BlazorShop.Api.Services.Users.Interfaces
{
    public interface IIndividualUserService
    {
        Task<OperationResult<ResponseLoginDto>> InsertIndividualUserAsync(IndividualUserRegistrationRequestDto requestRegistrationUserbusinessDto);
        Task<OperationResult<IndividualUser>> UpdateIndividualUserAsync(int id, IndividualUserUpdateRequestDto updateRegistrationUser);
    }
}
