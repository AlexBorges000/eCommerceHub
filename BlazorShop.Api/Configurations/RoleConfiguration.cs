using BlazorShop.Api.Entities;
using BlazorShop.Api.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorShop.Api.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasData(RoleSeed.Roles);
        }
    }
}
