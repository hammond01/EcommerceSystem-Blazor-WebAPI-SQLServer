using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Repositories.Interfaces
{
    public interface IAccountServices
    {
        public Task<object> LoginAsync(Login login);
        public Task<IdentityResult> Register(Register register);
        public Task<IdentityResult> RegisterEmployee(RegisterEmloyee registerEmloyee);
        public Task<object> GetRoles();

    }
}
