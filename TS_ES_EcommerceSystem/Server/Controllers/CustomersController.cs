using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class CustomersController(ICustomersServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var res = await _repo.GetCustomers();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            try
            {
                var res = await _repo.GetCustomer(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCustomer(Customers customers)
        {
            try
            {
                var res = await _repo.AddCustomer(customers);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, Customers customers)
        {
            try
            {
                var res = await _repo.UpdateCustomer(id, customers);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                var res = await _repo.DeleteCustomer(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
