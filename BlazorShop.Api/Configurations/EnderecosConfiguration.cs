using BlazorShop.Api.Entities;
using BlazorShop.Api.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorShop.Api.Configurations
{
    public class EnderecosConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasIndex(e => e.CEP);
            builder.HasIndex(e => e.UsuarioId);
            builder.HasData(EnderecoSeed.Endereco);
        }
    }
}
