using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class EnderecoRepository(AppDbContext context) : IEnderecoRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Endereco endereco)
    {
        await _context.Endereco.AddAsync(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountByUserIdAsync(int userId)
    {
        return await _context.Endereco
            .CountAsync(e => e.UsuarioId == userId);
        
    }

    public async Task DeleteAsync(Endereco endereco)
    {
         _context.Endereco.Remove(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteEndereco(Endereco endereco)
    {
        return await _context.Endereco
            .AnyAsync(e => e.UsuarioId == endereco.UsuarioId
            && e.CEP == endereco.CEP
            && e.Numero == endereco.Numero);
    }

    public async Task<IEnumerable<Endereco>> GetAllByUserIdAsync(int userId)
    {
        return await _context.Endereco
            .Where(x=> x.UsuarioId == userId)
            .ToListAsync();
    }

    public async Task<Endereco?> GetByUserIdAndIdAsync(int userId, int id)
    {
        return await _context.Endereco
            .FirstOrDefaultAsync(e => e.Id == id 
            && e.UsuarioId == userId);
    }

    public async Task<Endereco> UpdateAsync(Endereco endereco)
    {
        _context.Endereco.Update(endereco);
        await _context.SaveChangesAsync();
        return endereco;
    }
}
