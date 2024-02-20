using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models.ResponseModel;
using Models;
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

        public async Task<object> GetInformationInboundByWareHouseID(int id)
        {
            try
            {
                var query = @"SELECT 
                                si.InboundID,
	                            pb.ProductionBatchName,
	                            p.ProductName,
	                            u.UnitName,
	                            si.QuantityInbound,
	                            si.DateInbound,
	                            pb.ManufactureDate,
	                            pb.ExpiryDate
                            FROM StockInbound si
	                            LEFT JOIN ProductionBatch pb ON si.ProductionBatchID = pb.ProductionBatchID 
	                            LEFT JOIN Products p ON pb.ProductID = p.ProductID
	                            LEFT JOIN Units u ON pb.UnitID = u.UnitID
                            WHERE si.WareHouseID = @id";
                var res = await Program.Sql.QueryAsync<InformationStockInboundFromWarehouse>(query, new { id });
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

        public async Task<object> GetStockInbound(int id)
        {
            try
            {
                var query = @"SELECT * FROM StockInbound s LEFT JOIN ProductionBatch p ON s.ProductionBatchID = p.ProductionBatchID WHERE InboundID = @id;";
                var res = await Program.Sql.QueryAsync<StockInBoundResponse, ProductionBatch, StockInBoundResponse>(
                    query,
                    (inbound, productBatch) =>
                    {
                        inbound.ProductionBatch = productBatch;
                        return inbound;
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

        public async Task<object> GetStockInbounds()
        {
            try
            {
                var query = @"SELECT * FROM StockInbound s LEFT JOIN ProductionBatch p ON s.ProductionBatchID = p.ProductionBatchID ORDER BY s.InboundID DESC";
                var res = await Program.Sql.QueryAsync<StockInBoundResponse, ProductionBatch, StockInBoundResponse>(
                    query,
                    (inbound, productBatch) =>
                    {
                        inbound.ProductionBatch = productBatch;
                        return inbound;
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
