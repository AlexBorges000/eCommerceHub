using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByTaxIdHashAsync(string hashTaxId);
    Task<User?> GetByBusinessTaxIdHashAsync(string hashBusinessTaxId);
    Task<User?> GetByIdAsync(int id);
    Task<BusinessUser?> GetBusinessByIdAsync(int id);
    Task<IndividualUser ?> GetIndividualByIdAsync(int id);

    Task DeleteUserAsync(User user);
}
