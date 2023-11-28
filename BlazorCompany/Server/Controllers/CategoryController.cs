using BlazorCompany.Server.Repositories.UnitOfWork;
using BlazorCompany.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCompany.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET method for returning paginated list of CategoryDto objects
        [HttpGet]
        public IActionResult GetCategories(string? searchTerm, int pageIndex, int pageSize)
        {
            // Call CategoryRepository's method for returning paginated list of CategoryDto objects
            var pagination = unitOfWork.CategoryRepository.GetFilteredCategories(searchTerm ?? string.Empty, pageIndex, pageSize);

            // Return status code 200 along with list of CategoryDto objects
            return Ok(pagination);
        }

        // GET method for returning single CategoryDto object
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleCategory(int id)
        {
            // Find CategoryDto by calling CategoryRepository's method
            // for returnig single record
            CategoryDto categoryDto = await unitOfWork.CategoryRepository.GetSingleCategoryAsync(id);

            // If categoryDto is not null, return 200 status code along with categoryDto
            if (categoryDto != null)
            {
                return Ok(categoryDto);
            }
            // Otherwise return 404 (Not Found) status code
            else
            {
                return NotFound();
            }
        }

        // POST method for creating new Category
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory(CategoryDto categoryDto)
        {
            // Validate categoryDto using CategoryRepository's ValidateCategoryAsync method
            var errorCheck = await unitOfWork.CategoryRepository.ValidateCategoryAsync(categoryDto);

            // If there are any errors, return BadRequest status code along with errorCheck
            if (errorCheck.Any())
            {
                return BadRequest(errorCheck);
            }

            // Invoke CategoryRepository's method for creating new Category
            await unitOfWork.CategoryRepository.CreateNewCategoryAsync(categoryDto);
            // Save changes to database
            await unitOfWork.ConfirmChangesAsync();
            // Return OK result
            return Ok();
        }

        // POST method for updating selected Category
        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryDto categoryDto)
        {
            // Validate categoryDto using CategoryRepository's ValidateCategoryAsync method
            var errorCheck = await unitOfWork.CategoryRepository.ValidateCategoryAsync(categoryDto);

            // If there are any errors, return BadRequest status code along with errorCheck
            if (errorCheck.Any())
            {
                return BadRequest(errorCheck);
            }

            // Invoke CategoryRepository's method for creating new Category
            await unitOfWork.CategoryRepository.UpdateCategoryAsync(categoryDto);
            // Save changes to database
            await unitOfWork.ConfirmChangesAsync();
            // Return OK result
            return Ok();
        }

        // POST method for deleting selected Category
        [HttpPost]
        public async Task<IActionResult> DeleteCategory([FromBody] int id)
        {
            // Invoke CategoryRepository's method for deleting selected Category
            await unitOfWork.CategoryRepository.DeleteCategoryAsync(id);
            // Save changes to database
            await unitOfWork.ConfirmChangesAsync();
            // Return OK result
            return Ok();
        }


    }
}
