using API.Warehouse.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.WarehouseModel;
using Nest;

namespace API.Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController(IWarehouseServices _repo, ILogger<WarehousesController> _logger) : ControllerBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetWareHouse()
        {
            try
            {
                _logger.LogInformation($"Attempting to get warehouses");

                var res = await _repo.GetWareHouses();

                _logger.LogInformation($"Successfully retrieved warehouses");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting warehouses: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetWarehouse(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get warehouse with ID: {id}");

                var res = await _repo.GetWareHouse(id);

                _logger.LogInformation($"Successfully retrieved warehouse with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting warehouse with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddWarehouse(WareHouse warehouse)
        {
            try
            {
                _logger.LogInformation($"Attempting to add warehouse");

                var data = await _repo.AddWareHouse(warehouse);

                _logger.LogInformation($"Successfully add warehouse");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add warehouse: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWarehouse(int id, WareHouse warehouse)
        {
            try
            {
                _logger.LogInformation($"Attempting to update warehouse with ID {id}");

                var data = await _repo.UpdateWareHouse(id, warehouse);

                _logger.LogInformation($"Successfully update warehouse with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update warehouse: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete warehouse with ID {id}");

                var data = await _repo.DeleteWareHouse(id);

                _logger.LogInformation($"Successfully delete warehouse with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete warehouse with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
