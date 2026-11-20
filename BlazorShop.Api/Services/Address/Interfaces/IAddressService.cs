using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.Address;
using AddressEntity = BlazorShop.Api.Entities.Address;

namespace BlazorShop.Api.Services.Address.Interfaces;

public interface IAddressService
{
    Task<OperationResult<IEnumerable<AddressEntity>>> GetAllByUserIdAsync(int userId);
    Task<OperationResult<AddressEntity>> AddAsync(int userId, AddressAddRequestDto requestAddAddressDto);
    Task<OperationResult<AddressEntity>> UpdateAsync(int id, int userId, AddressUpdateRequestDto requestUpdateAddressDto);
    Task<OperationResult<AddressEntity>> DeleteAsync(int id, int userId);
    Task<OperationResult<AddressEntity>> GetAddressByUserIdAndIdAsync(int userId, int id);

}
