using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class OrderDetailsController(IOrderDetailsServices _repo, ILogger<OrderDetailsController> _logger) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetOrderDetails()
        {
            try
            {
                _logger.LogInformation($"Attempting to get orderDetail");

                var res = await _repo.GetOrderDetails();

                _logger.LogInformation($"Successfully retrieved orderDetail");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting orderDetail: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get orderDetail with ID: {id}");

                var res = await _repo.GetOrderDetail(id);

                _logger.LogInformation($"Successfully retrieved orderDetail with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting orderDetail with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddOrderDetail(OrderDetails orderDetail)
        {
            try
            {
                _logger.LogInformation($"Attempting to add orderDetail");

                var res = await _repo.AddOrderDetail(orderDetail);

                _logger.LogInformation($"Successfully add orderDetail");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add orderDetail: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetails orderDetail)
        {
            try
            {
                _logger.LogInformation($"Attempting to update orderDetail with ID {id}");

                var data = await _repo.UpdateOrderDetail(id, orderDetail);

                _logger.LogInformation($"Successfully update orderDetail with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update orderDetail: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{orderID}/{productID}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrderDetail(int orderID, int productID)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete orderDetail with orderID {orderID} and productID {productID}");

                var data = await _repo.DeleteOrderDetail(orderID, productID);

                _logger.LogInformation($"Successfully delete orderDetail with orderID {orderID} and productID {productID}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete orderDetail with orderID {orderID} and productID {productID}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
