using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; } = true;
    }
}