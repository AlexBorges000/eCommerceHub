namespace BlazorShop.Models.DTOs.OrderDtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public string? ProductImageURL { get; set; }
    public decimal Price { get; set; }
    public decimal PriceTotal { get; set; }
}
