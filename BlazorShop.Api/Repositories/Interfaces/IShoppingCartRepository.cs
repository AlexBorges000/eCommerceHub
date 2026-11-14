using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.CartDtos;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IShoppingCartRepository
{
    Task<OperationResult<CartItem>> AddItem(CartItemAddRequestDto cartItemAddDto);
    Task<OperationResult<CartItem>> UpdateQuantity(int id, CartItemQuantityUpdateRequestDto cartItemAtulizaQuantityDto);
    Task<OperationResult<CartItem>> DeleteItem(int id);
    Task<OperationResult<CartItem>> GetItem(int id);
    Task<OperationResult<IEnumerable<CartItem>>> GetItems(int userId);
}