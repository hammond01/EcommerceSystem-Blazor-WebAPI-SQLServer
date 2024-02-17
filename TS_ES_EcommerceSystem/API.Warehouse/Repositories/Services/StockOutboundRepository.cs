using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models.ResponseModel;
using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Services
{
    public class StockOutboundRepository : IStockOutboundServices
    {
        public async Task<object> AddStockOutbound(StockOutbound stockOutbound)
        {
            try
            {
                var query = Extension.GetInsertQuery("StockOutbound", "OutboundID", "DateOutbound", "ProductionBatchID", "QuantityOutbound", "Note");
                var data = await Program.Sql.QuerySingleAsync<StockOutbound>(query, stockOutbound);
                stockOutbound.OutboundID = data.OutboundID;
                return new
                {
                    data = stockOutbound,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteStockOutbound(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("StockOutbound", "OutboundID", id);
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

        public async Task<object> GetStockOutbound(int id)
        {
            try
            {
                var query = @"SELECT * FROM StockOutbound s LEFT JOIN ProductionBatch p ON s.ProductionBatchID = p.ProductionBatchID WHERE OutboundID = @id;";
                var res = await Program.Sql.QueryAsync<StockOutBoundResponse, ProductionBatch, StockOutBoundResponse>(
                    query,
                    (outbound, productBatch) =>
                    {
                        outbound.ProductionBatch = productBatch;
                        return outbound;
                    },
                    new { id },
                    splitOn: "ProductionBatchID"
                );
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

        public async Task<object> GetStockOutbounds()
        {
            try
            {
                var query = @"SELECT * FROM StockOutbound s LEFT JOIN ProductionBatch p ON s.ProductionBatchID = p.ProductionBatchID ORDER BY s.OutboundID DESC";
                var res = await Program.Sql.QueryAsync<StockOutBoundResponse, ProductionBatch, StockOutBoundResponse>(
                    query,
                    (outbound, productBatch) =>
                    {
                        outbound.ProductionBatch = productBatch;
                        return outbound;
                    },
                    splitOn: "ProductionBatchID"
                );

                return new
                {
                    data = res.AsList(),
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> UpdateStockOutbound(int id, StockOutbound stockOutbound)
        {
            try
            {
                var query = @"UPDATE StockOutbound SET 
                                DateOutbound = @DateOutbound, 
                                ProductionBatchID = @ProductionBatchID, 
                                QuantityOutbound = @QuantityOutbound, 
                                Note = @Note 
                                    WHERE OutboundID = @OutboundID;";
                stockOutbound.OutboundID = id;
                await Program.Sql.ExecuteAsync(query, stockOutbound);
                return new
                {
                    data = stockOutbound,
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
