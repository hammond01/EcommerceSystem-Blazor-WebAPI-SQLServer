using API.Warehouse.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.WarehouseModel;

namespace API.Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionBatchsController(IProductionBatchServices _repo, ILogger<WarehousesController> _logger) : ControllerBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetProductionBatch()
        {
            try
            {
                _logger.LogInformation($"Attempting to get productionBatchs");

                var res = await _repo.GetProductionBatchs();

                _logger.LogInformation($"Successfully retrieved productionBatchs");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting productionBatchs: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetWarehouse(string id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get productionBatch with ID: {id}");

                var res = await _repo.GetProductionBatch(id);

                _logger.LogInformation($"Successfully retrieved productionBatch with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting productionBatch with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddWarehouse(ProductionBatch productionBatch)
        {
            try
            {
                _logger.LogInformation($"Attempting to add productionBatch");

                var data = await _repo.AddProductionBatch(productionBatch);

                _logger.LogInformation($"Successfully add productionBatch");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add productionBatch: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWarehouse(string id, ProductionBatch productionBatch)
        {
            try
            {
                _logger.LogInformation($"Attempting to update productionBatch with ID {id}");

                var data = await _repo.UpdateProductionBatch(id, productionBatch);

                _logger.LogInformation($"Successfully update productionBatch with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update productionBatch: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteWarehouse(string id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete productionBatch with ID {id}");

                var data = await _repo.DeleteProductionBatch(id);

                _logger.LogInformation($"Successfully delete productionBatch with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete productionBatch with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
