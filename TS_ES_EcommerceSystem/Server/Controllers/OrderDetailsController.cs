using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class OrderDetailsController(IOrderDetailsServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetOrderDetails()
        {
            try
            {
                var res = await _repo.GetOrderDetails();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            try
            {
                var res = await _repo.GetOrderDetail(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddOrderDetail(OrderDetails order)
        {
            try
            {
                var data = await _repo.AddOrderDetail(order);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetails order)
        {
            try
            {
                var data = await _repo.UpdateOrderDetail(id, order);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{orderID}/{productID}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderID, int productID)
        {
            try
            {
                var data = await _repo.DeleteOrderDetail(orderID, productID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
