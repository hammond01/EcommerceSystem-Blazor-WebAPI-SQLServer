using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class SuppliersController(ISuppliersServices _repo, ILogger<SuppliersController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                _logger.LogInformation($"Attempting to get suppliers");

                var res = await _repo.GetSuppliers();

                _logger.LogInformation($"Successfully retrieved suppliers");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting suppliers: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get supplier with ID: {id}");

                var res = await _repo.GetSupplier(id);

                _logger.LogInformation($"Successfully retrieved supplier with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting supplier with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddSupplier(Suppliers supplier)
        {
            try
            {
                _logger.LogInformation($"Attempting to add supplier");

                var data = await _repo.AddSupplier(supplier);

                _logger.LogInformation($"Successfully add supplier");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add supplier: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSupplier(int id, Suppliers supplier)
        {
            try
            {
                _logger.LogInformation($"Attempting to update supplier with ID {id}");

                var data = await _repo.UpdateSupplier(id, supplier);

                _logger.LogInformation($"Successfully update supplier with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update supplier: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete supplier with supplierID {id}");

                var data = await _repo.DeleteSupplier(id);

                _logger.LogInformation($"Successfully delete supplier with supplierID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete supplier with supplierID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
