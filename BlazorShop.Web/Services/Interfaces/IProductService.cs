using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.ProductDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IProductService
{
    Task<OperationResult<IEnumerable<ProductRequestDto>>> GetItems();
    Task<OperationResult<ProductRequestDto>> GetItem(int id);
}
