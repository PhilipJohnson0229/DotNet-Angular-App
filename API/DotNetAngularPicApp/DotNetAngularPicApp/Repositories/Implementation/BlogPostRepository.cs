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
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }
    }
}
