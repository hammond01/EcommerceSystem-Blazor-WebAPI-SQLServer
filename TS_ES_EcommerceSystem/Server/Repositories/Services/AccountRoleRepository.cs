using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Server.Helper;
using Server.Helper.JWTModel;
using Server.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Repositories.Services
{
    public class AccountRoleRepository : IAccountRoleServices
    {
        private readonly Appsettings? _appsettings;
        public AccountRoleRepository(IOptionsMonitor<Appsettings> optionsMonitor)
        {
            _appsettings = optionsMonitor.CurrentValue;
        }



        public async Task<object> GetRoleByName(string name)
        {
            try
            {
                var query = @"SELECT * FROM Roles WHERE RoleName = @name;";
                var res = await Program.Sql.QuerySingleAsync<Role>(query, new { name });
                return new
                {
                    data = res,
                    status = 200,
                    msg = "Get role success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get customer: {ex.Message}");
            }
        }

        public async Task<object> GetRoles()
        {
            try
            {
                var query = @"SELECT * FROM Roles;";
                var res = (await Program.Sql.QueryAsync<Role>(query)).AsList();
                return new
                {
                    data = res,
                    status = 200,
                    msg = "Get roles success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get customer: {ex.Message}");
            }
        }
        public async Task<object> GetAccounts()
        {
            try
            {
                var query = @"SELECT * FROM Users;";
                var res = (await Program.Sql.QueryAsync<Users>(query)).AsList();
                return new
                {
                    data = res,
                    status = 200,
                    msg = "Get users success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get customer: {ex.Message}");
            }
        }
        public async Task<object> Register(Users users)
        {
            try
            {
                var usernameExistsQuery = @"SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                var usernameExists = await Program.Sql.ExecuteScalarAsync<int>(usernameExistsQuery, new Users { UserName = users.UserName });

                if (usernameExists > 0)
                {
                    return new
                    {
                        status = 400,
                        msg = "Username already exists. Please choose a different username."
                    };
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(users.Password);
                users.Password = hashedPassword;

                var query = Extension.GetInsertQuery("Users", "UserID", "UserName", "Password", "RoleID");

                var data = await Program.Sql.QueryFirstOrDefaultAsync<int>(query, users);
                users.UserID = data;
                return new
                {
                    data = users,
                    status = 200,
                    msg = "Register success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in register: {ex.Message}");
            }
        }

        public async Task<object> LoginUser(string userName, string password)
        {
            try
            {

                var hashedPasswordQuery = @"SELECT Password FROM Users WHERE UserName = @userName";
                var hashedPassword = await Program.Sql.QueryFirstOrDefaultAsync<string>(hashedPasswordQuery, new { userName });


                if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                {
                    var checkRoleAccountQuery = @"SELECT r.RoleName FROM Users u LEFT JOIN Roles r ON u.RoleID = r.RoleID WHERE u.UserName = @userName";
                    var role = await Program.Sql.QuerySingleOrDefaultAsync<string>(checkRoleAccountQuery, new { userName });
                    var check = _appsettings!.SecretKey;
                    return new
                    {
                        data = GenerateToken(userName, role!),
                        status = 200,
                        msg = "Login success!"
                    };
                }
                else
                {
                    return new
                    {
                        data = false,
                        status = 400,
                        msg = "Login fail!"
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get customer: {ex.Message}");
            }
        }

        private string GenerateToken(string userName, string roles)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appsettings!.SecretKey!);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim("TokenId", Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, roles)
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(9),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }


}
