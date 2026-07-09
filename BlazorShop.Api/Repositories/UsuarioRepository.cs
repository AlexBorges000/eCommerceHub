using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Entities.Enums;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordHasher;
    public UsuarioRepository(AppDbContext context, IPasswordService passwordRepository)
    {
        _context = context;
        _passwordHasher = passwordRepository;
    }

    public async Task<Usuario?> GetAsync(string email)
    {
        return await _context.Usuarios
                     .SingleOrDefaultAsync(u => u.Email == email);
    }
    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Id == id);
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

}