using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshTokens> RefreshTokens { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<IndividualUser> IndividualUsers { get; set; } = null!;
    public DbSet<BusinessUser> BusinessUsers { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Carrinhos");
            entity.Property(x => x.UserId).HasColumnName("UsuarioId");
        });
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("CarrinhoItens");
            entity.Property(x => x.CartId).HasColumnName("CarrinhoId");
            entity.Property(x => x.ProductId).HasColumnName("ProdutoId");
            entity.Property(x => x.Quantity).HasColumnName("Quantidade");
        });
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Produtos");
            entity.Ignore(x => x.Slug);
            entity.Property(x => x.Name).HasColumnName("Nome");
            entity.Property(x => x.ImageUrl).HasColumnName("ImagemUrl");
            entity.Property(x => x.Description).HasColumnName("Descricao");
            entity.Property(x => x.Price).HasColumnName("Preco");
            entity.Property(x => x.Quantity).HasColumnName("Quantidade");
            entity.Property(x => x.CategoryId).HasColumnName("CategoriaId");
        });
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categorias");
            entity.Property(x => x.Name).HasColumnName("Nome");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(x => x.Phone).HasColumnName("Telefone");
            entity.Property(x => x.Password).HasColumnName("Senha");
        });
        modelBuilder.Entity<IndividualUser>(entity =>
        {
            entity.Property(x => x.Name).HasColumnName("Nome");
            entity.Property(x => x.EncryptTaxId).HasColumnName("EncryptCpf");
            entity.Property(x => x.HashTaxId).HasColumnName("HashCpf");
        });
        modelBuilder.Entity<BusinessUser>(entity =>
        {
            entity.Property(x => x.TradeName).HasColumnName("NomeFantasia");
            entity.Property(x => x.LegalName).HasColumnName("RazaoSocial");
            entity.Property(x => x.PurchasingContact).HasColumnName("ResponsavelCompra");
            entity.Property(x => x.StateRegistration).HasColumnName("InscricaoEstadual");
            entity.Property(x => x.HashBusinessTaxId).HasColumnName("HashCNPJ");
            entity.Property(x => x.EncryptBusinessTaxId).HasColumnName("EncryptCNPJ");
        });
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Endereco");
            entity.Property(x => x.UserId).HasColumnName("UsuarioId");
            entity.Property(x => x.Street).HasColumnName("Logradouro");
            entity.Property(x => x.Number).HasColumnName("Numero");
            entity.Property(x => x.Complement).HasColumnName("Complemento");
            entity.Property(x => x.City).HasColumnName("Cidade");
            entity.Property(x => x.District).HasColumnName("Bairro");
            entity.Property(x => x.PostalCode).HasColumnName("CEP");
            entity.Property(x => x.IsPrimary).HasColumnName("Principal");
        });
        modelBuilder.Entity<RefreshTokens>(entity =>
        {
            entity.Property(x => x.ExpirationDate).HasColumnName("DataExpiracao");
            entity.Property(x => x.UserId).HasColumnName("UsuarioId");
        });
    }
}