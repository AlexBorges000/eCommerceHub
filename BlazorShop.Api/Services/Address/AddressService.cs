using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Address.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.Address;
using AddressEntity = BlazorShop.Api.Entities.Address;

namespace BlazorShop.Api.Services.Address
{
    public class AddressService(IAddressRepository addressRepository) : IAddressService
    {
        private readonly IAddressRepository _addressRepository = addressRepository;

        public async Task<OperationResult<AddressEntity>> AddAsync(int userId, AddressAddRequestDto requestAddAddressDto)
        {
            var address = new AddressEntity
            {
                Street = requestAddAddressDto.Street,
                District = requestAddAddressDto.District,
                Number = requestAddAddressDto.Number,
                PostalCode = requestAddAddressDto.PostalCode,
                City = requestAddAddressDto.City,
                Complement = requestAddAddressDto.Complement,
                IsPrimary = requestAddAddressDto.IsPrimary,
                UF = requestAddAddressDto.UF,
                UserId = userId,
            };

            if (await _addressRepository.ExistsAddress(address))
            {
                return OperationResult<AddressEntity>.Fail("Endereço ja cadastrado");
            }
            await _addressRepository.AddAsync(address);
            return OperationResult<AddressEntity>.Ok(address);
        }

        public async Task<OperationResult<AddressEntity>> DeleteAsync(int id, int userId)
        {
            var address = await _addressRepository.GetByUserIdAndIdAsync(userId, id);
            if (address is null)
            {
                return OperationResult<AddressEntity>.Fail("Endereço não encontrado");
            }

            if (address.IsPrimary)
            {
                return OperationResult<AddressEntity>.Fail("Defina outro endereço como principal para excluir esse");
            }

            var addresses = await _addressRepository.CountByUserIdAsync(userId);
            if (addresses == 1)
            {
                return OperationResult<AddressEntity>.Fail("Adicione mais um endereço para excluir este!");
            }

            await _addressRepository.DeleteAsync(address);
            return OperationResult<AddressEntity>.Ok(address);
        }

        public async Task<OperationResult<IEnumerable<AddressEntity>>> GetAllByUserIdAsync(int userId)
        {
            var addresses = await _addressRepository.GetAllByUserIdAsync(userId);
            if (addresses is null)
            {
                return OperationResult<IEnumerable<AddressEntity>>.Fail("Nenhum Endereço encontrado");
            }
            
            return OperationResult<IEnumerable<AddressEntity>>.Ok(addresses);
        }

        public async Task<OperationResult<AddressEntity>> GetAddressByUserIdAndIdAsync(int userId, int id)
        {
            var address = await _addressRepository.GetByUserIdAndIdAsync(userId, id);
            if (address is null)
            {
                return OperationResult<AddressEntity>.Fail("Endereço não encontrado");
            }

            return OperationResult<AddressEntity>.Ok(address);
        }

        public async Task<OperationResult<AddressEntity>> UpdateAsync(int id, int userId, AddressUpdateRequestDto requestUpdateAddressDto)
        {
            var address = await _addressRepository.GetByUserIdAndIdAsync(userId, id);
            if (address is null)
            {
                return OperationResult<AddressEntity>.Fail("Endereço não encontrado");
            }
            address.Street = requestUpdateAddressDto.Street ?? address.Street;
            address.Number = requestUpdateAddressDto.Number ?? address.Number;
            address.Complement = requestUpdateAddressDto.Complement ?? address.Complement;
            address.City = requestUpdateAddressDto.City ?? address.City;
            address.District = requestUpdateAddressDto.District ?? address.District;
            address.PostalCode = requestUpdateAddressDto.PostalCode ?? address.PostalCode;
            address.IsPrimary = requestUpdateAddressDto.IsPrimary ?? address.IsPrimary;

            await _addressRepository.UpdateAsync(address);
            return OperationResult<AddressEntity>.Ok(address);
        }
    }
}
