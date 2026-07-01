using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.ProdutoDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IProdutoService
{
    Task<OperationResult<IEnumerable<RequestGetProdutoDto>>> GetItens();
    Task<OperationResult<RequestGetProdutoDto>> GetItem(int id);
}
