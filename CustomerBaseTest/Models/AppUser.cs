using Microsoft.AspNetCore.Identity;

namespace CustomerBaseTest.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Avatar { get; set; }
        public string Password { get; set; }
    }
}
