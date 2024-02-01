using AuthenticationAPI.Models;
using AuthenticationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Repositories.Services
{
    public class AccountRepository : IAccountServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<IdentityResult> Register(Register register)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    UserName = register.Email
                };

                var result = await userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Customer);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

        }
        public async Task<object> LoginAsync(Login login)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(login.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user!, login.Password);
                if (user == null || !passwordValid)
                {
                    return new
                    {
                        data = "",
                        status = 500,
                        message = "Login fail"
                    };
                }
                var userRole = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Email, login.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));
                var token = new JwtSecurityToken
                    (
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(20),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature
                    ));
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                if (string.IsNullOrEmpty(jwt))
                {
                    return new
                    {
                        data = "",
                        status = 500,
                        message = "Login fail"
                    };
                }
                return new
                {
                    data = jwt,
                    status = 200,
                    message = "Login success"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<IdentityResult> RegisterEmployee(RegisterEmployee registerEmployee)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = registerEmployee.FirstName,
                    LastName = registerEmployee.LastName,
                    Email = registerEmployee.Email,
                    UserName = registerEmployee.Email
                };

                var result = await userManager.CreateAsync(user, registerEmployee.Password);

                if (result.Succeeded)
                {
                    var role = await roleManager.FindByIdAsync(registerEmployee.RoleID);
                    await userManager.AddToRoleAsync(user, role!.Name!);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<object> GetRoles()
        {
            try
            {
                var roles = await roleManager.Roles.ToListAsync();
                return new
                {
                    data = roles,
                    message = "Get all role success"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
