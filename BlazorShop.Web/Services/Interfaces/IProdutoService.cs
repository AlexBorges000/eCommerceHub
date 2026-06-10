using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services.Interfaces;

public interface IProdutoService
{
    Task<OperationResult<IEnumerable<ProdutoDto>>> GetItens();
    Task<OperationResult<ProdutoDto>> GetItem(int id);
}
