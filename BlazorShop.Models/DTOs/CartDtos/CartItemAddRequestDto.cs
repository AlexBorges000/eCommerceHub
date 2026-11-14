using System.ComponentModel.DataAnnotations;

namespace BlazorShop.Models.DTOs.CartDtos;

public class CartItemAddRequestDto
{
    [Required]
    public int CartId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
