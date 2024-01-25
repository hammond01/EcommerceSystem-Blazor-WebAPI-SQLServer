using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class ProductsController(IProductsServices _repo) : ConBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var res = await _repo.GetProducts();
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
                var res = await _repo.GetProduct(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(Products product)
        {
            try
            {
                var res = await _repo.AddProduct(product);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            try
            {
                var res = await _repo.UpdateProduct(id, product);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var res = await _repo.DeleteProduct(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
