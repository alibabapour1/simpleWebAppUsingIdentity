using Microsoft.AspNetCore.Identity;

namespace simpleWebAppUsingIdentity.Models.Entities
{
    public class Roles:IdentityRole
    {
        public string Description { get; set; }
    }
}
