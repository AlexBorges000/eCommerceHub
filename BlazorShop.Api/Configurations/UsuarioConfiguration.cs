using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorShop.Api.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasOne(x => x.Carrinho)
            .WithOne(x => x.Usuario)
            .HasForeignKey<Carrinho>(x => x.UsuarioId);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Usuario)
                .HasForeignKey(x => x.RoleId);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
