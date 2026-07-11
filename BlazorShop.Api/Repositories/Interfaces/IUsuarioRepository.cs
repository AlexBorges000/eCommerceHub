using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task<Usuario?> GetAsync(string email);
    Task<Usuario?> GetByIdAsync(int id);
    Task DeleteUsuarioAsync(Usuario usuario);
}
