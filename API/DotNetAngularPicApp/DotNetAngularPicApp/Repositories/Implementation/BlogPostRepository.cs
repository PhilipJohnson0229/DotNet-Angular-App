using DotNetAngularPicApp.Data;
using DotNetAngularPicApp.Models.Domain;
using DotNetAngularPicApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularPicApp.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            //with this line we include the categories related to the blogposts
            //.Include()
            return await dbContext.BlogPosts.Include(b => b.Categories).ToListAsync();
        }

        //REMINDER the question mark is syntax for an object that can return in place of null values
        //just like the Optional type in java
        public async Task<BlogPost?> GetById(Guid id)
        {
            //pay attention to this pattern its similiar to how we can handle collections with
            //streams in java
            return await dbContext.BlogPosts.Include(b => b.Categories).FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
