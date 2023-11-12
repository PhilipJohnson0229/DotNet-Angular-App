using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularPicApp.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        //Seeding the data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "413808e8-ed0c-4edb-869f-ca7967fead34";
            var writerRoleId = "e6bc8c2b-8b9c-4c5e-ab83-781617a675c5";

            //Create reading and writing roles
            var roles = new List<IdentityRole> {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed the roles wtht he builder object provided with this method
            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "de08439b-f4ad-4167-9577-89d0eef3ddd8";

            //Create an admin user
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Admin",
                Email = "philip.johnson0229@gmail.com",
                NormalizedEmail = "Admin".ToUpper(),
                NormalizedUserName = "philip.johnson0229@gmail.com".ToUpper(),
                AccessFailedCount = 3
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            //Seed the roles wtht he builder object provided with this method
            builder.Entity<IdentityUser>().HasData(admin);

            //Give roles to the admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
