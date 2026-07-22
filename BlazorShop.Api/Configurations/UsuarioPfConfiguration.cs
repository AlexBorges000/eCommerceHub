using BlazorShop.Api.Entities;
using BlazorShop.Api.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BlazorShop.Api.Configurations
{
    public class UsuarioPfConfiguration : IEntityTypeConfiguration<UsuarioFisico>
    {
        public void Configure(EntityTypeBuilder<UsuarioFisico> builder)
        {
            builder.HasIndex(x => x.HashCpf).IsUnique();
            builder.ToTable("UsuarioFisico");
            builder.HasData(UsuarioPfSeed.Usuario);
        }
    }
}
