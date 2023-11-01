﻿using Azure.Core;
using DotNetAngularPicApp.Models.Domain;
using DotNetAngularPicApp.Models.Dto;
using DotNetAngularPicApp.Repositories.Implementation;
using DotNetAngularPicApp.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAngularPicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository, 
            ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        //POST https://localhost:7092/api/Categories
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            //convert from dto to domain model
            //obviously we wont be passing the Id because that is autogenerated by the dbContext
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                ShortDescription = request.ShortDescription,
                PublishedDate = request.PublishedDate,
                Title = request.Title,
                IsVisible = request.IsVisible,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            //Loop through all of these IDs and ensure were getting the right values from the client
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                //existingCategory was found in the database
                if(existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            //convert the domain model to a dto
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                UrlHandle = blogPost.UrlHandle,
                //we use Select to convert from domain model to Dto
                //again this is much like using stream in java
                Categories = blogPost.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle= x.UrlHandle,

                }).ToList(),
            };

            return Ok(response);
        }

        //GET https://localhost:7092/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            //never expose class model just expose DTOs
            var blogPosts = await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();

            //Map domain model to DTO
            foreach (var blogPost in blogPosts)
            {
                // loop through all of the categories in the db and add to the response object
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Content = blogPost.Content,
                    PublishedDate = blogPost.PublishedDate,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    ShortDescription = blogPost.ShortDescription,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible,
                    UrlHandle = blogPost.UrlHandle,
                });
            }
            

            return Ok(response);
        }

    }
}
