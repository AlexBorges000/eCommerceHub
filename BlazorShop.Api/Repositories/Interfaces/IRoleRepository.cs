using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetRoleByNameAsync(string nome);
    Task<Role?> GetRoleByIdAsync(int roleId);
}
