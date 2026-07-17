using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IEnderecoRepository
{
    Task<IEnumerable<Endereco>> GetAllByUserIdAsync(int userId);
    Task<Endereco> UpdateAsync(Endereco endereco);
    Task DeleteAsync(Endereco endereco);
    Task AddAsync(Endereco endereco);
    Task<bool> ExisteEndereco(Endereco endereco);
    Task<int> CountByUserIdAsync(int userId);
    Task<Endereco?> GetByUserIdAndIdAsync(int userId, int id);
}
