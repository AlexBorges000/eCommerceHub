using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
                     .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<UsuarioFisico?> GetPfByIdAsync(int id)
    {
        return await _context.UsuariosFisicos.FindAsync(id);
    }

    public async Task<UsuarioJuridico?> GetPjByIdAsync(int id)
    {
        return await _context.UsuariosJuridicos.FindAsync(id);
    }

    public async Task AddAsync(Usuario usuario)
    {

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario?> GetByCpfHashAsync(string hashCpf)
    {
        return await _context.UsuariosFisicos
            .SingleOrDefaultAsync(u => u.HashCpf == hashCpf);
    }
    public async Task<Usuario?> GetByCnpjHashAsync(string hashCnpj)
    {
        return await _context.UsuariosJuridicos
            .SingleOrDefaultAsync(u => u.HashCNPJ == hashCnpj);
    }
}