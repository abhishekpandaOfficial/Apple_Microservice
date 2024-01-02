using Microsoft.AspNetCore.Identity;

namespace Apple.Services.AuthAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? NameOfUser { get; set; }

        public DateTime CreatedUser { get; set; }
    }
}
