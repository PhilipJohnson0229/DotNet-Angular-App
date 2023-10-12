using DotNetAngularPicApp.Data;
using DotNetAngularPicApp.Models.Domain;
using DotNetAngularPicApp.Models.Dto;
using DotNetAngularPicApp.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAngularPicApp.Controllers
{
    //this route translates to https://localhost:xxxx/api/Categories
    [Route("api/[controller]")]
    [ApiController]
    //all controllers will inherit from the ControllerBase class
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        //the command ctor then tab will create a constructor for you
        //create a constructor so we can leverage dependency injecton to wire in the services
        //that we need

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //to define an endpoint you need this header above the method
        //Task is much like a ResponseEntity type in java spring boot
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request) 
        {
            //convert input param Dto into a domain model so we can communicate with the db
            var category = new Category
            {
                //were not setting the id because entiy framework core is able to interpret and 
                //ignore exposing that because it is set automatically
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            //abstracting implementation to our repository
            await categoryRepository.CreateAsync(category);

            //map from the domain model to the dto so we do not expose buisness logic
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }
}
