using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAllByUserIdAsync(int userId);
    Task<Address> UpdateAsync(Address address);
    Task DeleteAsync(Address address);
    Task AddAsync(Address address);
    Task<bool> ExistsAddress(Address address);
    Task<int> CountByUserIdAsync(int userId);
    Task<Address?> GetByUserIdAndIdAsync(int userId, int id);
}
