using DotNetAngularPicApp.Data;
using DotNetAngularPicApp.Models.Domain;
using DotNetAngularPicApp.Repositories.Interface;

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
    }
}
