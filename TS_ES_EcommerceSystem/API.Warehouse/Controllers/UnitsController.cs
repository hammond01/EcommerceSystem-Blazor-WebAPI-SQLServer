using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ResponseModel;
using Models.WarehouseModel;

namespace API.Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController(ILogger<WarehousesController> _logger) : ControllerBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetUnits()
        {
            try
            {
                _logger.LogInformation($"Attempting to get units");
                var query = @"SELECT * FROM Units";
                var res = (await Program.Sql.QueryAsync<Units>(query)).AsList();
                _logger.LogInformation($"Successfully retrieved units");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting units: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}
