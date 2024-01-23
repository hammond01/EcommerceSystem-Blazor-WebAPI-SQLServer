using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class ShippersController(IShippersServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetShippers()
        {
            try
            {
                var res = await _repo.GetShippers();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetShipper(int id)
        {
            try
            {
                var res = await _repo.GetShipper(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddShipper(Shippers shippers)
        {
            try
            {
                var res = await _repo.AddShipper(shippers);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateShipper(int id, Shippers shippers)
        {
            try
            {
                var res = await _repo.UpdateShipper(id, shippers);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            try
            {
                var res = await _repo.DeleteShipper(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
