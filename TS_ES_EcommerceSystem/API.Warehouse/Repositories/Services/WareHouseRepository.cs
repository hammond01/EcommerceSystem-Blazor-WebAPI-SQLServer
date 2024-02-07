using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models;
using Models.WarehouseModel;
using Nest;

namespace API.Warehouse.Repositories.Services
{
    public class WareHouseRepository : IWarehouseServices
    {
        public async Task<object> AddWareHouse(WareHouse wareHouse)
        {
            try
            {
                var query = Extension.GetInsertQuery("Warehouse", "WareHouseID", "CostPrice", "ProductionBatchID", "QuantityTotal");
                var data = await Program.Sql.QuerySingleAsync<WareHouse>(query, wareHouse);
                wareHouse.WareHouseID = data.WareHouseID;
                return new
                {
                    data = wareHouse,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteWareHouse(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Warehouse", "WareHouseID", id);
                await Program.Sql.ExecuteAsync(query);
                return new
                {
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> GetWareHouse(int id)
        {
            try
            {
                var query = @"SELECT * FROM Warehouse WHERE WareHouseID = @id;";
                var res = await Program.Sql.QuerySingleAsync<WareHouse>(query, new { id });
                return new
                {
                    data = res,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> GetWareHouses()
        {
            try
            {
                var query = @"SELECT * FROM Warehouse";
                var res = (await Program.Sql.QueryAsync<WareHouse>(query)).AsList();
                return new
                {
                    data = res,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> UpdateWareHouse(int id, WareHouse wareHouse)
        {
            try
            {
                var query = @"UPDATE Warehouse SET 
                                CostPrice = @CostPrice, 
                                ProductionBatchID = @ProductionBatchID, 
                                QuantityTotal = @QuantityTotal 
                                    WHERE WareHouseID = @WareHouseID;";
                wareHouse.WareHouseID = id;
                await Program.Sql.ExecuteAsync(query, wareHouse);
                return new
                {
                    data = wareHouse,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
