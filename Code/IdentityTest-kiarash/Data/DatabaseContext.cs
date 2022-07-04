using IdentityTest.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityTest.Data
{
    public class DatabaseContext : IdentityDbContext<User,Role,string>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new {p.ProviderKey,p.LoginProvider});
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId , p.LoginProvider });

            modelBuilder.Entity<User>().Ignore(p=>p.NormalizedEmail);



        }
    }
}
