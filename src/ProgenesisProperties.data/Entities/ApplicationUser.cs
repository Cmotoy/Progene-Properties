using Microsoft.AspNetCore.Identity;

namespace ProgenesisProperties.data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {get; set;}
    }
}