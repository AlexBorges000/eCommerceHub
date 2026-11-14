using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CartDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IShoppingCartService
{
    Task<OperationResult<List<CartItemDto>>> GetItems(int userId);
    Task<OperationResult<CartItemDto>> AddItem(CartItemAddRequestDto cartItemAddDto);
    Task<OperationResult<CartItemDto>> DeleteItem(int id);
    Task<OperationResult<CartItemDto>> UpdateQuantity(CartItemQuantityUpdateRequestDto cartItemUpdateQuantityDto);
}
