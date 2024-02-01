using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Data
{
    public class RoleInitializer
    {

        /// <summary>
        /// Creates a new role if it does not already exist.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="roleName">The name of the role.</param>
        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        /// <summary>
        /// Initializes the role data.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
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
