using API.Warehouse.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.WarehouseModel;

namespace API.Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailWarehousesController(IDetailWarehouseServices _repo, ILogger<DetailWarehousesController> _logger) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddDetailWarehouse(DetailWarehouse obj)
        {
            try
            {
                _logger.LogInformation($"Attempting to add detailWarehouse");

                var data = await _repo.AddDetailWarehouse(obj);

                _logger.LogInformation($"Successfully add detailWarehouse");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add detailWarehouse: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateDetailWarehouse(int id, DetailWarehouse detailWarehouse)
        {
            try
            {
                _logger.LogInformation($"Attempting to update detailWarehouse with ID {id}");

                var data = await _repo.UpdateDetailWarehouse(id, detailWarehouse);

                _logger.LogInformation($"Successfully update detailWarehouse with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update detailWarehouse: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDetailWarehouse(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete detailWarehouse with ID {id}");

                var data = await _repo.DeleteDetailWarehouse(id);

                _logger.LogInformation($"Successfully delete detailWarehouse with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete detailWarehouse with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
