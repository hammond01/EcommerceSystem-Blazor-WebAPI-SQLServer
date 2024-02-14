using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Services
{
    public class ProductionBatchRepository : IProductionBatchServices
    {
        public async Task<object> AddProductionBatch(ProductionBatch productionBatch)
        {
            try
            {
                var query = Extension.GetInsertQuery("ProductionBatch", "ProductionBatchID", "ProductionBatchID", "ProductID", "Quantity", "ManufactureDate", "ExpiryDate");
                var data = await Program.Sql.QuerySingleAsync<ProductionBatch>(query, productionBatch);
                productionBatch.ProductionBatchID = data.ProductionBatchID;
                return new
                {
                    data = productionBatch,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteProductionBatch(string id)
        {
            try
            {
                var query = Extension.GetDeleteQueryString("ProductionBatch", "ProductionBatchID", id);
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

        public async Task<object> GetProductionBatch(string id)
        {
            try
            {
                var query = @"SELECT * FROM ProductionBatch WHERE ProductionBatchID = @id;";
                var res = await Program.Sql.QuerySingleAsync<ProductionBatch>(query, new { id });
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

        public async Task<object> GetProductionBatchs()
        {
            try
            {
                var query = @"SELECT * FROM ProductionBatch";
                var res = (await Program.Sql.QueryAsync<ProductionBatch>(query)).AsList();
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

        public async Task<object> UpdateProductionBatch(string id, ProductionBatch productionBatch)
        {
            try
            {
                var query = @"UPDATE ProductionBatch SET 
                                ProductID = @ProductID, 
                                Quantity = @Quantity, 
                                ManufactureDate = @ManufactureDate, 
                                ExpiryDate = @ExpiryDate 
                                    WHERE ProductionBatchID = @ProductionBatchID;";
                productionBatch.ProductionBatchID = id;
                await Program.Sql.ExecuteAsync(query, productionBatch);
                return new
                {
                    data = productionBatch,
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
