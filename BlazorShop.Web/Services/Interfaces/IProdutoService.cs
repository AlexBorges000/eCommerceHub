using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.ProdutoDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IProdutoService
{
    Task<OperationResult<IEnumerable<ProdutoDto>>> GetItens();
    Task<OperationResult<ProdutoDto>> GetItem(int id);
}
