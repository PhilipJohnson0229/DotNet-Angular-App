using DotNetAngularPicApp.Data;
using DotNetAngularPicApp.Models.Domain;
using DotNetAngularPicApp.Models.Dto;
using DotNetAngularPicApp.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAngularPicApp.Controllers
{
    //this route translates to https://localhost:7092/api/Categories
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
            //this will make calls to the repo interface which will in turn call the implementation
            //class.  so this is much like the service class to db layer pattern in spring
            this.categoryRepository = categoryRepository;
        }


        //to define an endpoint you need this header above the methodwith the HTTP verb
        //Task is much like a ResponseEntity type in java spring boot
        //POST https://localhost:7092/api/Categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
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


        //GET https://localhost:7092/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            //never expose class model just expose DTOs
            var categories = await categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();

            //Map domain model to DTO
            foreach (var category in categories)
            {
                // loop through all of the categories in the db and add to the response object
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }


        //GET https://localhost:7092/api/Categories/{Id}
        [HttpGet]
        //this i show we add query params to the url
        [Route("{id:Guid}")]
        //[FromRoute] grabs the query param from the url
        //I found that if we dont name the query param variable exactly as how it should be in the
        //route then .net core will get confused and require 2 query params making the method unusable
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id);

            //instead of the double equals operator we can use keywords like is
            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
            };

            return Ok(response);
        }


        //PUT https://localhost:7092/api/Categories/{Id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id,
            UpdateCategoryRequestDto request)
        {
            //Map dto to the domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await categoryRepository.UpdateAsync(category);

            if (category is null)
            {
                return NotFound();
            }

            //convert domain model to dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        //Generally its better not to use DELETE but for the purpose of this demo it will be used
        //its better practice to set another field to act as a flag to disable or enable the record
        //that flag can be used to filter out disabled and active records
        //DELETE https://localhost:7092/api/Categories/{Id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle    
            };

            return Ok(response);
        }
    }
}
