using CodeAPI.Data;
using CodeAPI.Models.Domain;
using CodeAPI.Models.DTO;
using CodeAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAPI.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await _categoryRepository.CreateAsync(category);

            //Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            return Ok(response);
        }

        //GET /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
           var categories =  await _categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();
           
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id=category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
           return Ok(response);
        }
        //GET category base ID
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryRepository.FindByIdAsync(id);

            if(category == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }
        // update category by Id
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            //convert Dto to domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            await _categoryRepository.UpdateAsync(category);

            if(category == null)
            {
                return NotFound();
            }

            //convert domain Model to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }
        // Delete category
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _categoryRepository.DeleteCategory(id);

            if (category == null)
            {
                return NotFound();

            }   
            //convert  Domain to DTO
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
