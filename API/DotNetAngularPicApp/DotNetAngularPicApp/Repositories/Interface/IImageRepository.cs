using DotNetAngularPicApp.Models.Domain;

namespace DotNetAngularPicApp.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);

        Task<IEnumerable<BlogImage>> GetAll();
    }
}
