using DotNetAngularPicApp.Models.Domain;

namespace DotNetAngularPicApp.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        //the question mark is much like the optional type in java in that it can return as null
        Task<BlogPost?> GetByIdAsync(Guid id);

        //
        Task<BlogPost?> UpdateByIdAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
