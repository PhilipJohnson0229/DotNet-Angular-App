using DotNetAngularPicApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularPicApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        //this construtor will be called in child classes
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
