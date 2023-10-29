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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        //REMINDER the question mark is syntax for an object that can return in place of null values
        //just like the Optional type in java
        public async Task<Category?> GetById(Guid id)
        {
            //pay attention to this pattern its similiar to how we can handle collections with
            //streams in java
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCatgory = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == category.Id);

            if(existingCatgory != null)
            {
                dbContext.Entry(existingCatgory).CurrentValues.SetValues(category);

                //remember that we need this line if we plan to make changes in the db
                await dbContext.SaveChangesAsync();

                return category;
            }

            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            
            if(existingCategory is null)
            {
                return null;
            }

            dbContext.Categories.Remove(existingCategory);

            //again dont forget this line so we can update the db
            await dbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
