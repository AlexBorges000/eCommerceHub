using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class RoleRepository(AppDbContext appDbContext) : IRoleRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Role.FindAsync(roleId);
    }

    public async Task<Role?> GetRoleByNameAsync(string nome)
    {
        return await _context.Role
                     .SingleOrDefaultAsync(r => r.Name == nome); 
    }
}
    