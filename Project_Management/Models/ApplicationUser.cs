using Microsoft.AspNetCore.Identity;

namespace Project_Management.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; } = null!;
    }
}
