using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class CategoryController(ICategoriesServices _repo, ILogger<CategoryController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                _logger.LogInformation($"Attempting to get categories");

                var res = await _repo.GetCategories();

                _logger.LogInformation($"Successfully retrieved categories");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting categories: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get category with ID: {id}");

                var res = await _repo.GetCategory(id);

                _logger.LogInformation($"Successfully retrieved category with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting category with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory(Categories category)
        {
            try
            {
                _logger.LogInformation($"Attempting to add category with:{category}");

                var data = await _repo.AddCategory(category);

                _logger.LogInformation($"Successfully add category with:{category}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add category: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, Categories category)
        {
            try
            {
                _logger.LogInformation($"Attempting to update category with ID {id}, {category}");

                var data = await _repo.UpdateCategory(id, category);

                _logger.LogInformation($"Successfully update category with ID {id}, {category}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update category: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete category with ID {id}");

                var data = await _repo.DeleteCategory(id);

                _logger.LogInformation($"Successfully delete category with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete category with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
