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
    public class UnitsController : ControllerBase
    {
        [HttpGet("Gets")]
        public async Task<IActionResult> GetUnits()
        {
            try
            {
                var query = @"SELECT * FROM Units";
                var res = (await Program.Sql.QueryAsync<Units>(query)).AsList();
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
