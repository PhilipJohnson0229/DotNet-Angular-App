using DotNetAngularPicApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularPicApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        //this construtor will be called in child classes
        //because we have multiple DbContext's we have to specify within the DbContextOtions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //These are required in order for the entity manager to create the tables in the db
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
