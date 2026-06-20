using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IProdutoRepository
{
    Task<OperationResult<IEnumerable<Produto>>> GetItens();
    Task<OperationResult<Produto>> GetItem(int id);
    Task<OperationResult<IEnumerable<Produto>>> GetItensPorCategoria(int id);
}
