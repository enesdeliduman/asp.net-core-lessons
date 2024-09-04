using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0} alanı minimum {2}, maksimum {1} karakter olmalıdır", MinimumLength = 3)]
        [Display(Name = "Kullanıcı adı")]
        public string? UserName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} alanı minimum {2}, maksimum {1} karakter olmalıdır", MinimumLength = 3)]
        [Display(Name = "İsim")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} alanı minimum {2}, maksimum {1} karakter olmalıdır", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmiyor")]
        [Display(Name = "Parola tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}