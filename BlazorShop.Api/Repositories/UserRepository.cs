using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
                     .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IndividualUser?> GetIndividualByIdAsync(int id)
    {
        return await _context.IndividualUsers.FindAsync(id);
    }

    public async Task<BusinessUser?> GetBusinessByIdAsync(int id)
    {
        return await _context.BusinessUsers.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByTaxIdHashAsync(string hashTaxId)
    {
        return await _context.IndividualUsers
            .SingleOrDefaultAsync(u => u.HashTaxId == hashTaxId);
    }
    public async Task<User?> GetByBusinessTaxIdHashAsync(string hashBusinessTaxId)
    {
        return await _context.BusinessUsers
            .SingleOrDefaultAsync(u => u.HashBusinessTaxId == hashBusinessTaxId);
    }
}