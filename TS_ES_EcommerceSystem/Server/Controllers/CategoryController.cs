using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class CategoryController(ICategoriesServices _repo) : ConBase
    {
        [HttpGet("Gets")]
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
        [HttpGet("Get/{id}")]
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
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory(Categories category)
        {
            try
            {
                var data = await _repo.AddCategory(category);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Categories category)
        {
            try
            {
                var data = await _repo.UpdateCategory(id, category);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var data = await _repo.DeleteCategory(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
