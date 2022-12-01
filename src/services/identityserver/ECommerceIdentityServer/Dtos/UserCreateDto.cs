using ECommerceIdentityServer.Models;

namespace ECommerceIdentityServer.Dtos
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Locale { get; set; }
        public string Password { get; set; }

        public static implicit operator ApplicationUser(UserCreateDto user)
        {
            return new ApplicationUser
            { 
                UserName = user.UserName, 
                Email= user.Email,
                Locale = user.Locale,
                
            };
        }
    }
}
