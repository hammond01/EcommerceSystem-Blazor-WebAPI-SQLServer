using System.ComponentModel.DataAnnotations;

namespace Models.RequestModel
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}