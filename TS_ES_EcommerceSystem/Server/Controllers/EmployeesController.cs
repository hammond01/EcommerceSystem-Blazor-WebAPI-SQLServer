using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class EmployeesController(IEmloyeesServices _repo, ILogger<EmployeesController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetEmployees(int page = 1, int pageSize = 10, string fullName = "")
        {
            try
            {
                _logger.LogInformation($"Attempting to get employees with page = {page}, pageSize = {pageSize}, contactName = {fullName}");

                var res = await _repo.GetEmployees(page, pageSize, fullName);

                _logger.LogInformation($"Successfully retrieved employees  with page = {page}, pageSize = {pageSize}, contactName = {fullName}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting employees: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get employee with ID: {id}");

                var res = await _repo.GetEmployee(id);

                _logger.LogInformation($"Successfully retrieved employee with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting employee with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployee(Employees employees)
        {
            try
            {
                _logger.LogInformation($"Attempting to add employee");

                var data = await _repo.AddEmployee(employees);

                _logger.LogInformation($"Successfully add employee");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add employee: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEmployee(int id, Employees employees)
        {
            try
            {
                _logger.LogInformation($"Attempting to update employee with ID {id}");

                var data = await _repo.UpdateEmployee(id, employees);

                _logger.LogInformation($"Successfully update employee with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update employee: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete employee with ID {id}");

                var res = await _repo.DeleteEmployee(id);

                _logger.LogInformation($"Successfully delete employee with ID {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete employee with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
