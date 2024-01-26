using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModel
{
    public class Login
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
