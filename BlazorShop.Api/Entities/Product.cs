using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorShop.Api.Entities;

public class Product
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(200)]
    public string ImageUrl { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Description {  get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    [MaxLength(250)]
    public string Slug { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
            = new List<CartItem>();
}