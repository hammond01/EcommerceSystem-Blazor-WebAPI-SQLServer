using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Server.Helper;
using Server.Repositories.Interfaces;
using System.Text;

namespace Server.Controllers
{
    public class ProductsController(IProductsServices _repo, ILogger<ProductsController> _logger, WebHookConfig _webHookConfig) : ConBase
    {
        private List<string> _webhookUrls = new List<string>();
        [HttpGet("Gets")]
        public async Task<IActionResult> GetProducts(int page = 1, int pageSize = 10, string productName = "")
        {
            try
            {
                _logger.LogInformation($"Attempting to get products with page = {page}, pageSize = {pageSize}, productName = {productName}");

                var res = await _repo.GetProducts(page, pageSize, productName);

                _logger.LogInformation($"Successfully retrieved products with page = {page}, pageSize = {pageSize}, productName = {productName}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting products with page = {page}, pageSize = {pageSize}, productName = {productName}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get product with ID: {id}");

                var res = await _repo.GetProduct(id);

                _logger.LogInformation($"Successfully retrieved product with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting product with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-product/{id}")]
        public async Task<IActionResult> GetProductEdit(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get product with ID: {id}");

                var res = await _repo.GetProductEdit(id);

                _logger.LogInformation($"Successfully retrieved product with ID: {id}");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting product with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(Products product)
        {
            try
            {
                _logger.LogInformation($"Attempting to add product");

                var res = await _repo.AddProduct(product);

                _logger.LogInformation($"Successfully add product");

                _webHookConfig.NotifyWebhookApi(product, "addProduct");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting add product: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("Update/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            try
            {
                _logger.LogInformation($"Attempting to update product with ID {id}");

                var data = await _repo.UpdateProduct(id, product);

                _logger.LogInformation($"Successfully update product with ID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting update product: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete product with productID {id}");

                var data = await _repo.DeleteProduct(id);

                _logger.LogInformation($"Successfully delete product with productID {id}");

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting delete product with productID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
