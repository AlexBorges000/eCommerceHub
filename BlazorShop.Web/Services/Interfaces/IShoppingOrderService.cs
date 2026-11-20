using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.OrderDtos;

namespace BlazorShop.Web.Services.Interfaces;

public interface IShoppingOrderService
{
    Task<OperationResult<List<OrderItemDto>>> GetItems(int userId);
    Task<OperationResult<OrderItemDto>> AddItem(OrderItemAddRequestDto orderItemAddDto);
    Task<OperationResult<OrderItemDto>> DeleteItem(int id);
    Task<OperationResult<OrderItemDto>> UpdateQuantity(OrderItemQuantityUpdateRequestDto orderItemUpdateQuantityDto);
}
