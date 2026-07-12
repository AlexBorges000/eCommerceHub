using BlazorShop.Api.Entities;
using BlazorShop.Api.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorShop.Api.Configurations
{
    public class UsuarioPjConfiguration : IEntityTypeConfiguration<UsuarioJuridico>
    {
        public void Configure(EntityTypeBuilder<UsuarioJuridico> builder)
        {
            builder.HasIndex(x => x.HashCNPJ).IsUnique();
            builder.ToTable("UsuarioJuridico");
            builder.HasData(UsuarioPjSeed.Usuario);
        }
    }
}
