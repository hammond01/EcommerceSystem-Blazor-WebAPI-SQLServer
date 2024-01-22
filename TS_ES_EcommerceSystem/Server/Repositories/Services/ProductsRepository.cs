using Dapper;
using Models;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class ProductsRepository : IProductsServices
    {
        public async Task<object> AddProduct(Products product)
        {
            try
            {
                var query = Extension.GetInsertQuery("Products", "ProductID", "ProductName", "SupplierID", "CategoryID",
                    "QuantityPerUnit", "UnitPrice", "UnitsInStock", "UnitsOnOrder", "ReorderLevel", "Discontinued");

                var data = await Program.Sql.QuerySingleAsync<Products>(query, product);
                product.ProductID = data.ProductID;
                return new
                {
                    data = product,
                    status = 200,
                    msg = "Add product success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in add new product: {ex.Message}");
            }
        }

        public async Task<object> GetProduct(int id)
        {
            try
            {
                var query = @"SELECT *
                                FROM Products p
                                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                                LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                                    WHERE ProductID = @id;";
                var res = await Program.Sql.QueryAsync<Products, Categories, Suppliers, Products>(query,
                    (product, category, supplier) =>
                    {
                        product.Categories = category;
                        product.Suppliers = supplier;
                        return product;
                    },
                    new { id },
                    splitOn: "CategoryID, SupplierID"
                    );
                return new
                {
                    data = res,
                    status = 200,
                    msg = "Get product success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get product: {ex.Message}");
            }
        }

        public async Task<object> GetProducts()
        {
            try
            {
                var query = @"SELECT *
                                FROM Products p
                                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                                LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID";
                var res = await Program.Sql.QueryAsync<Products, Categories, Suppliers, Products>(query,
                    (product, category, supplier) =>
                    {
                        product.Categories = category;
                        product.Suppliers = supplier;
                        return product;
                    },
                    splitOn: "CategoryID, SupplierID"
                    );
                return new
                {
                    data = res.AsList(),
                    status = 200,
                    msg = "Get products success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get products: {ex.Message}");
            }
        }

        public async Task<object> UpdateProduct(int id, Products product)
        {
            try
            {
                var query = @"UPDATE Products SET 
                                ProductName = @ProductName, 
                                SupplierID = @SupplierID, 
                                CategoryID = @CategoryID, 
                                QuantityPerUnit = @QuantityPerUnit, 
                                UnitPrice = @UnitPrice, 
                                UnitsInStock = @UnitsInStock, 
                                UnitsOnOrder = @UnitsOnOrder, 
                                ReorderLevel = @ReorderLevel, 
                                Discontinued = @Discontinued 
                                WHERE ProductID = @ProductID;";
                product.ProductID = id;
                await Program.Sql.ExecuteAsync(query, product);
                return new
                {
                    data = product,
                    status = 0,
                    msg = "Update product success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in update product: {ex.Message}");
            }
        }
        public async Task<object> DeleteProduct(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Products", "ProductID", id);
                await Program.Sql.ExecuteAsync(query);
                return new
                {
                    status = 200,
                    msg = $"Delete product with ProductID {id} success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in delete product: {ex.Message}");
            }
        }
    }
}
