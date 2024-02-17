using API.Warehouse.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.WarehouseModel;

namespace API.Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockOutboundsController(IStockOutboundServices _repo, ILogger<WarehousesController> _logger) : ControllerBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetStockOutbounds()
        {
            try
            {
                _logger.LogInformation($"Attempting to get stockOutbounds");

                var res = await _repo.GetStockOutbounds();

                _logger.LogInformation($"Successfully retrieved stockOutbounds");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting stockOutbounds: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetStockOutbound(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get stockOutbound with ID: {id}");

                var res = await _repo.GetStockOutbound(id);

                _logger.LogInformation($"Successfully retrieved stockOutbound with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting stockOutbound with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddStockOutbound(StockOutbound stockOutbound)
        {
            try
            {
                _logger.LogInformation($"Attempting to add stockOutbound");

                var data = await _repo.AddStockOutbound(stockOutbound);

                _logger.LogInformation($"Successfully add stockOutbound");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add stockOutbound: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateStockOutbound(int id, StockOutbound stockOutbound)
        {
            try
            {
                _logger.LogInformation($"Attempting to update stockOutbound with ID {id}");

                var data = await _repo.UpdateStockOutbound(id, stockOutbound);

                _logger.LogInformation($"Successfully update stockOutbound with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update stockOutbound: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteStockOutbound(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete stockOutbound with ID {id}");

                var data = await _repo.DeleteStockOutbound(id);

                _logger.LogInformation($"Successfully delete stockOutbound with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete stockOutbound with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
