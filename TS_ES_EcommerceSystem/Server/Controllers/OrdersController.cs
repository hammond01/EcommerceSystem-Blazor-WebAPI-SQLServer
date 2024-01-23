using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class OrdersController(IOrdersServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetOrders(int page = 1, int pageSize = 10)
        {
            try
            {
                var res = await _repo.GetOrders(page, pageSize);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var res = await _repo.GetOrder(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder(Orders order)
        {
            try
            {
                var data = await _repo.AddOrder(order);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Orders order)
        {
            try
            {
                var data = await _repo.UpdateOrder(id, order);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var data = await _repo.DeleteOrder(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
