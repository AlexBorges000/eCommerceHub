using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Login;
using BlazorShop.Models.DTOs.UserDtos.Update;
namespace BlazorShop.Api.Mappings.Users;

public static class UserMappers
{
    public static BusinessUser ToEntity(this BusinessUserRegistrationRequestDto dto)
    {
        return new BusinessUser
        {
            Email = dto.Email,
            Phone = dto.Phone,
            TradeName = dto.TradeName,
            LegalName = dto.LegalName,
            StateRegistration = dto.StateRegistration,
            PurchasingContact = dto.PurchasingContact
        };
    }
    public static IndividualUser ToEntity(this IndividualUserRegistrationRequestDto dto)
    {
        return new IndividualUser
        {
            Email = dto.Email,
            Phone = dto.Phone,
            Name = dto.Name,
        };
    }
    public static void UpdateEntity(this IndividualUserUpdateRequestDto dto, IndividualUser entity)
    {
        if (!string.IsNullOrWhiteSpace(dto.Phone))
            entity.Phone = dto.Phone;
    }

    public static void UpdateEntity(this RequestUpdateBusinessUserDto dto, BusinessUser entity)
    {
        if (dto.Phone != null)
            entity.Phone = dto.Phone;

        if (dto.LegalName != null)
            entity.LegalName = dto.LegalName;

        if (dto.PurchasingContact != null)
            entity.PurchasingContact = dto.PurchasingContact;

        if (dto.TradeName != null)
            entity.TradeName = dto.TradeName;
    }

}
