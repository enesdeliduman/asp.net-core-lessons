using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
