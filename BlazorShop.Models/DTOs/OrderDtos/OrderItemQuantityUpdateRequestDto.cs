namespace BlazorShop.Models.DTOs.OrderDtos;

public class OrderItemQuantityUpdateRequestDto
{
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
}
