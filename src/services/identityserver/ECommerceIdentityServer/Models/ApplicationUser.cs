using Microsoft.AspNetCore.Identity;

namespace ECommerceIdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Locale { get; set; }
    }
}
