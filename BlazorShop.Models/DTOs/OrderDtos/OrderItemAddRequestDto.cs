using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models.DTOs.OrderDtos;

public class OrderItemAddRequestDto
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
