using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;

namespace BlazorShop.Api.Services.Auth.Interfaces;

public interface IRoleService
{
    Task<OperationResult<Role>> GetClienteRoleAsync();
    Task<OperationResult<Role>> GetRoleAsync(int roleId);
}
