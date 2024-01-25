using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Data
{
    public class RoleInitializer
    {
        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            await CreateRoleIfNotExists(roleManager, Roles.Customer);
            await CreateRoleIfNotExists(roleManager, Roles.Admin);
            await CreateRoleIfNotExists(roleManager, Roles.Manager);
            await CreateRoleIfNotExists(roleManager, Roles.Accountant);
            await CreateRoleIfNotExists(roleManager, Roles.HR);
        }
    }
}
