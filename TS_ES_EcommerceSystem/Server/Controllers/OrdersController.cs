using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class OrdersController(IOrdersServices _repo, ILogger<OrdersController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetOrders(int page = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation($"Attempting to get orders");

                var res = await _repo.GetOrders(page, pageSize);

                _logger.LogInformation($"Successfully retrieved orders");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting orders: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get order with ID: {id}");

                var res = await _repo.GetOrder(id);

                _logger.LogInformation($"Successfully retrieved order with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting order with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder(Orders order)
        {
            try
            {
                _logger.LogInformation($"Attempting to add order");

                var res = await _repo.AddOrder(order);

                _logger.LogInformation($"Successfully add order");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add order: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(int id, Orders order)
        {
            try
            {
                _logger.LogInformation($"Attempting to update order with ID {id}");

                var data = await _repo.UpdateOrder(id, order);

                _logger.LogInformation($"Successfully update order with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update order: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete order with orderID {id}");

                var data = await _repo.DeleteOrder(id);

                _logger.LogInformation($"Successfully delete order with orderID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete order with orderID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
