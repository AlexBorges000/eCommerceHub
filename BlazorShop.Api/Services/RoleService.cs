using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
namespace BlazorShop.Api.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    public async Task<OperationResult<Role>> GetClienteRoleAsync()
    {
        var role = await _roleRepository.GetRoleByNameAsync("Cliente");

        if (role == null)
        {
            throw new InvalidOperationException("A role padrão não foi encontrada.");
        }

        return OperationResult<Role>.Ok(role);
    }

    public async Task<OperationResult<Role>> GetRoleAsync(int roleId)
    {
        var role = await _roleRepository.GetRoleByIdAsync(roleId);

        if (role == null)
        {
            return OperationResult<Role>.Fail("A role não foi encontrada");
        }
        return OperationResult<Role>.Ok(role);
    }

}
