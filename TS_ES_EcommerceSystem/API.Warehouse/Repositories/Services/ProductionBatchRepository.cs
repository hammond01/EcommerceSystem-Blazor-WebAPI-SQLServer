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
                var query = Extension.GetInsertQuery("ProductionBatch", "ProductionBatchID", "ProductionBatchName", "ProductID", "Quantity", "UnitID", "ManufactureDate", "ExpiryDate");
                var productionBatchName = Extension.RamdomNumber();
                var check = await ProductBatchNameExists(productionBatchName);
                while (check)
                {
                    productionBatchName = Extension.RamdomNumber();
                    check = await ProductBatchNameExists(productionBatchName);
                }
                productionBatch.ProductionBatchName = productionBatchName;
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
                var query = @"SELECT * FROM ProductionBatch pb 
                                        LEFT JOIN Products p 
                                            ON pb.ProductID = p.ProductID 
                                        LEFT JOIN Units u 
                                            ON pb.UnitID = u.UnitID
                                        WHERE ProductionBatchID = @id;";
                var res = await Program.Sql.QueryAsync<ProductBathResponse, Products, Units, ProductBathResponse>(
                    query,
                    (productionBatch, product, unit) =>
                    {
                        productionBatch.Products = product;
                        productionBatch.Units = unit;
                        return productionBatch;
                    },
                    new { id },

                    splitOn: "ProductID, UnitID"
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
                var query = @"SELECT * FROM ProductionBatch pb 
                                            LEFT JOIN Products p 
                                                ON pb.ProductID = p.ProductID 
                                            LEFT JOIN Units u 
                                                ON pb.UnitID = u.UnitID
                                            ORDER BY pb.ProductionBatchID DESC";
                var res = await Program.Sql.QueryAsync<ProductBathResponse, Products, Units, ProductBathResponse>(
                    query,
                    (productionBatch, product, unit) =>
                    {
                        productionBatch.Products = product;
                        productionBatch.Units = unit;
                        return productionBatch;
                    },
                    splitOn: "ProductID, UnitID"
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
                                UnitID = @UnitID,
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
        public async Task<bool> ProductBatchNameExists(string productBatchName)
        {
            try
            {
                var query = @"SELECT COUNT(*) FROM ProductionBatch WHERE ProductionBatchName = @ProductionBatchName";
                var count = await Program.Sql.ExecuteScalarAsync<int>(query, new { ProductionBatchName = productBatchName });

                return count > 0;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
                Console.WriteLine($"Error checking existence of product batch name: {ex.Message}");
                throw;
            }
        }
    }


}
