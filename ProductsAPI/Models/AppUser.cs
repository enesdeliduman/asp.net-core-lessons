using Microsoft.AspNetCore.Identity;

namespace ProductsAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
