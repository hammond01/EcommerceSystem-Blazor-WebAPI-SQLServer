using System.ComponentModel.DataAnnotations;

namespace Server.Helper.JWTModel
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
        [Required]
        [MaxLength(250)]
        public string Password { get; set; } = null!;
    }
}
