using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<OperationResult<IEnumerable<Product>>> GetItems();
    Task<OperationResult<Product>> GetItem(int id);
    Task<OperationResult<IEnumerable<Product>>> GetItemsPorCategory(int id);
}
