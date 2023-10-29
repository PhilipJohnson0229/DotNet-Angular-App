using DotNetAngularPicApp.Models.Domain;

namespace DotNetAngularPicApp.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync();

        //the question mark is much like the optional type in java in that it can return as null
        Task<Category?> GetById(Guid id);

        Task<Category?> UpdateAsync(Category category);
    }
}
