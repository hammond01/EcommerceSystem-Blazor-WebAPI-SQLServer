using Models;
using Server.Helper.JWTModel;

namespace Server.Repositories.Interfaces
{
    public interface IAccountRoleServices
    {
        public Task<object> GetRoles();
        public Task<object> GetRoleByName(string name);
        public Task<object> Register(Users users);
        public Task<object> LoginUser(LoginModel loginModel);
        /*public Task<object> ForgotPasswordUser(Users users);
        //Information customer or employee
        public Task<object> GetInfomationAccount(Users users);*/
        public Task<object> GetAccounts();
    }
}
