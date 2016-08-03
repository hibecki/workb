using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PPtest.Models;

namespace PPtest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ForSqlServerToTable("Users");
            builder.Entity<IdentityUserRole<string>>().ForSqlServerToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ForSqlServerToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ForSqlServerToTable("UserClaims");
            builder.Entity<IdentityRole>().ForSqlServerToTable("Roles");

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity().Map(c =>
            //{
            //    c.ToTable("UserLogin");
            //    c.Properties(p => new
            //    {
            //        p.UserId,
            //        p.LoginProvider,
            //        p.ProviderKey
            //    });
            //}).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });
        }
    }
}
