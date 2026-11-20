namespace BlazorShop.Api.Entities
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsRevoked { get; set; }
    }
}
