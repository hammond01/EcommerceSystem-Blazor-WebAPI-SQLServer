using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class ShippersController(IShippersServices _repo, ILogger<ShippersController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetShippers()
        {
            try
            {
                _logger.LogInformation($"Attempting to get shippers");

                var res = await _repo.GetShippers();

                _logger.LogInformation($"Successfully retrieved shippers");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting shippers: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetShipper(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get shipper with shipperID: {id}");

                var res = await _repo.GetShipper(id);

                _logger.LogInformation($"Successfully retrieved shipper with shipperID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting shipper with shipperID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddShipper(Shippers shippers)
        {
            try
            {
                _logger.LogInformation($"Attempting to add shipper");

                var res = await _repo.AddShipper(shippers);

                _logger.LogInformation($"Successfully add shipper");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add shipper: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateShipper(int id, Shippers shippers)
        {
            try
            {
                _logger.LogInformation($"Attempting to update shipper with shipperID {id}");

                var res = await _repo.UpdateShipper(id, shippers);

                _logger.LogInformation($"Successfully update shipper with shipperID {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update shipper: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete shipper with shipperID {id}");

                var res = await _repo.DeleteShipper(id);

                _logger.LogInformation($"Successfully delete shipper with shipperID {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete shipper with shipperID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
