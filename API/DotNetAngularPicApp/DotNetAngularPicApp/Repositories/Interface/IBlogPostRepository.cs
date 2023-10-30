using DotNetAngularPicApp.Models.Domain;

namespace DotNetAngularPicApp.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
