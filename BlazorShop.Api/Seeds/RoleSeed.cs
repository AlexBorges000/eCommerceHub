using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Seeds;

public class RoleSeed
{
    public static readonly Role[] Roles = CriarRoles();
    private static Role[] CriarRoles()
    {
        return
        [
            new Role
            {
                Id = 1,
                Name = "Cliente"
            },

            new Role
            {
                Id = 2,
                Name = "Administrador"
            }
         ];
    }
}
