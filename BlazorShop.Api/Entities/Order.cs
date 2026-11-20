namespace BlazorShop.Api.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();
    public User User { get; set; } = null!;
}
