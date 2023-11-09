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
        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            //pay attention to this pattern its similiar to how we can handle collections with
            //streams in java
            return await dbContext.BlogPosts.Include(b => b.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateByIdAsync(BlogPost blogPost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories)
               .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            // Update BlogPost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            // Update Categories
            existingBlogPost.Categories = blogPost.Categories;

            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                dbContext.BlogPosts.Remove(existingBlogPost);
                await dbContext.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }

    }
}