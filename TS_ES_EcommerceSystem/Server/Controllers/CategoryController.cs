using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class CategoryController(ICategoriesServices _repo) : ConBase
    {
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var res = await _repo.GetCategories();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var res = await _repo.GetCategory(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(Categories category)
        {
            try
            {
                var id = await _repo.AddCategory(category);
                var categoryNew = await _repo.GetCategory(id);
                return categoryNew == null ? NotFound() : Ok(categoryNew);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Categories category)
        {
            try
            {
                await _repo.UpdateCategory(id, category);
                return Ok("Update category successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _repo.DeleteCategory(id);
                return Ok("Delete category with CategoryID: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
