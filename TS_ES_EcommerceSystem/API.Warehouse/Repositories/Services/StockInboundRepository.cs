using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Services
{
    public class StockInboundRepository : IStockInboundServices
    {
        public async Task<object> AddStockInbound(StockInbound stockInbound)
        {
            try
            {
                var query = Extension.GetInsertQuery("StockInbound", "InboundID", "DateInbound", "ProductionBatchID", "QuantityInbound", "Note");
                var data = await Program.Sql.QuerySingleAsync<StockInbound>(query, stockInbound);
                stockInbound.InboundID = data.InboundID;
                return new
                {
                    data = stockInbound,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteStockInbound(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("StockInbound", "InboundID", id);
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

        public async Task<object> GetStockInbound(int id)
        {
            try
            {
                var query = @"SELECT * FROM StockInbound WHERE InboundID = @id;";
                var res = await Program.Sql.QuerySingleAsync<StockInbound>(query, new { id });
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

        public async Task<object> GetStockInbounds()
        {
            try
            {
                var query = @"SELECT * FROM StockInbound";
                var res = (await Program.Sql.QueryAsync<StockInbound>(query)).AsList();
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

        public async Task<object> UpdateStockInbound(int id, StockInbound stockInbound)
        {
            try
            {
                var query = @"UPDATE StockInbound SET 
                                DateInbound = @DateInbound, 
                                ProductionBatchID = @ProductionBatchID, 
                                QuantityInbound = @QuantityInbound,
                                Note = @Note 
                                    WHERE InboundID = @InboundID;";
                stockInbound.InboundID = id;
                await Program.Sql.ExecuteAsync(query, stockInbound);
                return new
                {
                    data = stockInbound,
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
