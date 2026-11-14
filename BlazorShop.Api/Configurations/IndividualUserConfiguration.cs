using BlazorShop.Api.Entities;
using BlazorShop.Api.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BlazorShop.Api.Configurations
{
    public class IndividualUserConfiguration : IEntityTypeConfiguration<IndividualUser>
    {
        public void Configure(EntityTypeBuilder<IndividualUser> builder)
        {
            builder.HasIndex(x => x.HashTaxId).IsUnique();
            builder.ToTable("UsuarioFisico");
            builder.HasData(IndividualUserSeed.User);
        }
    }
}
