using API.Warehouse.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.WarehouseModel;

namespace API.Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockInboundsController(IStockInboundServices _repo, ILogger<WarehousesController> _logger) : ControllerBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetStockInbounds()
        {
            try
            {
                _logger.LogInformation($"Attempting to get stockInbounds");

                var res = await _repo.GetStockInbounds();

                _logger.LogInformation($"Successfully retrieved stockInbounds");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting stockInbounds: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetStockInbound(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get stockInbound with ID: {id}");

                var res = await _repo.GetStockInbound(id);

                _logger.LogInformation($"Successfully retrieved stockInbound with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting stockInbound with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddStockInbound(StockInbound stockInbound)
        {
            try
            {
                _logger.LogInformation($"Attempting to add stockInbound");

                var data = await _repo.AddStockInbound(stockInbound);

                _logger.LogInformation($"Successfully add stockInbound");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add stockInbound: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateStockInbound(int id, StockInbound stockInbound)
        {
            try
            {
                _logger.LogInformation($"Attempting to update stockInbound with ID {id}");

                var data = await _repo.UpdateStockInbound(id, stockInbound);

                _logger.LogInformation($"Successfully update stockInbound with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update stockInbound: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteStockInbound(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete stockInbound with ID {id}");

                var data = await _repo.DeleteStockInbound(id);

                _logger.LogInformation($"Successfully delete stockInbound with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete stockInbound with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
