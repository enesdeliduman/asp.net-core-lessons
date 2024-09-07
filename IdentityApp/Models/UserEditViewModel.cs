using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models
{
    public class UserEditViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parola eslesmiyor")]
        public string? ConfirmPassword { get; set; }

        public IList<string>? SelectedRoles { get; set; }
    }
}