using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class CustomersController(ICustomersServices _repo, ILogger<CustomersController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetCustomers(int page = 1, int pageSize = 10, string contactName = "")
        {
            try
            {
                _logger.LogInformation($"Attempting to get customers with page = {page}, pageSize = {pageSize}, contactName = {contactName}");

                var res = await _repo.GetCustomers(page, pageSize, contactName);

                _logger.LogInformation($"Successfully retrieved customers  with page = {page}, pageSize = {pageSize}, contactName = {contactName}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting customers: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get customer with ID: {id}");

                var res = await _repo.GetCustomer(id);

                _logger.LogInformation($"Successfully retrieved customer with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting customer with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCustomer(Customers customers)
        {
            try
            {
                _logger.LogInformation($"Attempting to add customer");

                var res = await _repo.AddCustomer(customers);

                _logger.LogInformation($"Successfully add customer");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add customer: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer(string id, Customers customers)
        {
            try
            {
                _logger.LogInformation($"Attempting to update customer with ID {id}");

                var res = await _repo.UpdateCustomer(id, customers);

                _logger.LogInformation($"Successfully update customer with ID {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update customer: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete customer with ID {id}");

                var res = await _repo.DeleteCustomer(id);

                _logger.LogInformation($"Successfully delete customer with ID {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete customer with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
