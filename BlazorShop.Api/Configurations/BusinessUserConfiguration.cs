using BlazorShop.Api.Entities;
using BlazorShop.Api.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorShop.Api.Configurations
{
    public class BusinessUserConfiguration : IEntityTypeConfiguration<BusinessUser>
    {
        public void Configure(EntityTypeBuilder<BusinessUser> builder)
        {
            builder.HasIndex(x => x.HashBusinessTaxId).IsUnique();
            builder.ToTable("UsuarioJuridico");
            builder.HasData(BusinessUserSeed.User);
        }
    }
}
