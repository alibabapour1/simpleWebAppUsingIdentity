using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simpleWebAppUsingIdentity.Models.Entities;

namespace simpleWebAppUsingIdentity.Models.DbContext
{
    public class MyDbContext:IdentityDbContext<User,Roles,string>
    {
        public MyDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>().HasKey(k => new { k.LoginProvider, k.ProviderKey });
            builder.Entity<IdentityUserRole<string>>().HasKey(k => new { k.UserId, k.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(k => new { k.UserId, k.LoginProvider, k.Name });




        }
    }
}
