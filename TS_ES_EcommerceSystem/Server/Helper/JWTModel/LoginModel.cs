using System.ComponentModel.DataAnnotations;

namespace Server.Helper.JWTModel
{
    public class LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
