using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Models.DTO;
using CodeAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            //convert DTO to Domain
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsPublished = request.IsPublished,
            };
            await _blogPostRepository.CreateAsync(blogPost);

            //convert domain model to dto
            var reponse = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsPublished = blogPost.IsPublished,
            };
            return Ok();
        }
    }
}
