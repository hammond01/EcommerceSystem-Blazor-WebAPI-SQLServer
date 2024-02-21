using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Services
{
    public class DetailWarehouseRepository : IDetailWarehouseServices
    {
        public async Task<object> AddDetailWarehouse(DetailWarehouse wareHouse)
        {
            try
            {
                var query = Extension.GetInsertQuery("DetailWarehouse", "DetailWarehouseID", "WarehouseID", "ActualWarehouse", "CostPrice", "ProductionBatchID", "Note");

                var data = await Program.Sql.QuerySingleAsync<DetailWarehouse>(query, wareHouse);
                wareHouse.WarehouseID = data.WarehouseID;
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

        public async Task<object> DeleteDetailWarehouse(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("DetailWarehouse", "DetailWarehouseID", id);
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

        public Task<object> GetDetailWarehouse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDetailWarehouses()
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateDetailWarehouse(int id, DetailWarehouse wareHouse)
        {
            throw new NotImplementedException();
        }
    }
}
