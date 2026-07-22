using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByCpfHashAsync(string hashCpf);
    Task<Usuario?> GetByCnpjHashAsync(string hashCnpj);
    Task<Usuario?> GetByIdAsync(int id);
    Task<UsuarioJuridico?> GetPjByIdAsync(int id);
    Task<UsuarioFisico ?> GetPfByIdAsync(int id);

    Task DeleteUsuarioAsync(Usuario usuario);
}
