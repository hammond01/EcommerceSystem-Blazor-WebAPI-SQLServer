using API.Warehouse.Repositories.Interfaces;
using Dapper;
using Heplers;
using Models;
using Models.ResponseModel;
using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Services
{
    public class ProductionBatchRepository : IProductionBatchServices
    {
        public async Task<object> AddProductionBatch(ProductionBatch productionBatch)
        {
            try
            {
                var query = Extension.GetInsertQuery("ProductionBatch", "ProductionBatchID", "ProductionBatchName", "ProductID", "Quantity", "ManufactureDate", "ExpiryDate");
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

        public async Task<object> DeleteProductionBatch(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("ProductionBatch", "ProductionBatchID", id);
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

        public async Task<object> GetProductionBatch(int id)
        {
            try
            {
                var query = @"SELECT * FROM ProductionBatch pb LEFT JOIN Products p ON pb.ProductID = p.ProductID WHERE ProductionBatchID = @id;";
                var res = await Program.Sql.QueryAsync<ResProductionBatch, Products, ResProductionBatch>(
                    query,
                    (productionBatch, product) =>
                    {
                        productionBatch.Products = product;
                        return productionBatch;
                    },
                    new { id },

                    splitOn: "ProductID"
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

        public async Task<object> GetProductionBatchs()
        {
            try
            {
                var query = @"SELECT * FROM ProductionBatch pb LEFT JOIN Products p ON pb.ProductID = p.ProductID";
                var res = await Program.Sql.QueryAsync<ResProductionBatch, Products, ResProductionBatch>(
                    query,
                    (productionBatch, product) =>
                    {
                        productionBatch.Products = product;
                        return productionBatch;
                    },
                    splitOn: "ProductID"
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

        public async Task<object> UpdateProductionBatch(int id, ProductionBatch productionBatch)
        {
            try
            {
                var query = @"UPDATE ProductionBatch SET 
                                ProductID = @ProductID,                                 
                                ProductionBatchName = @ProductionBatchName, 
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
