using DotNetAngularPicApp.Data;
using DotNetAngularPicApp.Models.Domain;
using DotNetAngularPicApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularPicApp.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        //the command ctor then tab will create a constructor for you
        //create a constructor so we can leverage dependency injecton to wire in the services
        //that we need
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            //these two lines are needed to update the db
            //async methods require this line
            await dbContext.Categories.AddAsync(category);
            //entity framework uses this line to save changes in the db
            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
