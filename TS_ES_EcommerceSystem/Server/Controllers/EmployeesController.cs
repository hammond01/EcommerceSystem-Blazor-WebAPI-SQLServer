using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class EmployeesController(IEmloyeesServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var res = await _repo.GetEmployees();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var res = await _repo.GetEmployee(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddEmployee(Employees employees)
        {
            try
            {
                var data = await _repo.AddEmployee(employees);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEmployee(int id, Employees employees)
        {
            try
            {
                var data = await _repo.UpdateEmployee(id, employees);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var data = await _repo.DeleteEmployee(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
