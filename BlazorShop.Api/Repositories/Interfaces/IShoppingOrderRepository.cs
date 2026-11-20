using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.OrderDtos;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IShoppingOrderRepository
{
    Task<OperationResult<OrderItem>> AddItem(OrderItemAddRequestDto orderItemAddDto);
    Task<OperationResult<OrderItem>> UpdateQuantity(int id, OrderItemQuantityUpdateRequestDto orderItemAtulizaQuantityDto);
    Task<OperationResult<OrderItem>> DeleteItem(int id);
    Task<OperationResult<OrderItem>> GetItem(int id);
    Task<OperationResult<IEnumerable<OrderItem>>> GetItems(int userId);
}