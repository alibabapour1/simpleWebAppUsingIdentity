using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simpleWebAppUsingIdentity.Models.Entities;

namespace simpleWebAppUsingIdentity.Models.DbContext
{
    public class MyDbContext:IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
