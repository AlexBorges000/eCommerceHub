using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class AddressRepository(AppDbContext context) : IAddressRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountByUserIdAsync(int userId)
    {
        return await _context.Addresses
            .CountAsync(e => e.UserId == userId);
        
    }

    public async Task DeleteAsync(Address address)
    {
         _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAddress(Address address)
    {
        return await _context.Addresses
            .AnyAsync(e => e.UserId == address.UserId
            && e.PostalCode == address.PostalCode
            && e.Number == address.Number);
    }

    public async Task<IEnumerable<Address>> GetAllByUserIdAsync(int userId)
    {
        return await _context.Addresses
            .Where(x=> x.UserId == userId)
            .ToListAsync();
    }

    public async Task<Address?> GetByUserIdAndIdAsync(int userId, int id)
    {
        return await _context.Addresses
            .FirstOrDefaultAsync(e => e.Id == id 
            && e.UserId == userId);
    }

    public async Task<Address> UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
        return address;
    }
}
