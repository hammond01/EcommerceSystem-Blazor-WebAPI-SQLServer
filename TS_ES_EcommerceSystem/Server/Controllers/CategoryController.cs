using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoriesServices _repo) : ControllerBase
    {
        [HttpGet("GetCategories")]
        public IActionResult GetCategorys()
        {
            try
            {
                var res = _repo.GetCategorys();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
