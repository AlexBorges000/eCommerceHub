namespace BlazorShop.Api.Entities
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public bool IsRevoked { get; set; }
    }
}
