using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class SuppliersController(ISuppliersServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                var res = await _repo.GetSuppliers();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            try
            {
                var res = await _repo.GetSupplier(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddSupplier(Suppliers supplier)
        {
            try
            {
                var data = await _repo.AddSupplier(supplier);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSupplier(int id, Suppliers supplier)
        {
            try
            {
                var data = await _repo.UpdateSupplier(id, supplier);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var data = await _repo.DeleteSupplier(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
