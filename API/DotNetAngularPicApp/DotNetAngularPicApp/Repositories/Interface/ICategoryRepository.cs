using DotNetAngularPicApp.Models.Domain;

namespace DotNetAngularPicApp.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
