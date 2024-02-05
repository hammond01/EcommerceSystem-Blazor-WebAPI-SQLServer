using Dapper;
using Models;
using Models.RequestModel;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class ProductsRepository : IProductsServices
    {
        public async Task<object> AddProduct(ProductRequest product)
        {
            try
            {
                var query = Extension.GetInsertQuery("Products", "ProductID", "ProductName", "SupplierID", "CategoryID",
                    "QuantityPerUnit", "UnitPrice", "UnitsInStock", "UnitsOnOrder", "ReorderLevel", "Discontinued");

                var data = await Program.Sql.QuerySingleAsync<ProductRequest>(query, product);
                product.ProductID = data.ProductID;
                return new
                {
                    data = product,
                    status = 200
                };
            }
            catch
            {
                throw;
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
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> GetProductEdit(int id)
        {
            try
            {
                var query = @"SELECT *
                                FROM Products p
                                    WHERE ProductID = @id;";
                var res = await Program.Sql.QuerySingleAsync<Products>(query, new { id }
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

        public async Task<object> GetProducts(int page, int pageSize, string productName)
        {
            try
            {
                var offset = (page - 1) * pageSize;

                var query = @"SELECT p.*, c.*, s.*
                      FROM Products p
                      LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                      LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                      WHERE (@productName IS NULL OR p.ProductName LIKE '%' + @productName + '%')
                      ORDER BY p.ProductID
                      OFFSET @offset ROWS
                      FETCH NEXT @pageSize ROWS ONLY;";

                var totalProduct = @"SELECT COUNT(*) FROM Products";
                var resTotalProduct = await Program.Sql.QuerySingleAsync<int>(totalProduct);

                double totalPage = resTotalProduct / pageSize;

                int roundedTotalPage = (int)Math.Ceiling(totalPage);

                var parameters = new { productName, offset, pageSize };

                var res = await Program.Sql.QueryAsync<Products, Categories, Suppliers, Products>(
                    query,
                    (product, category, supplier) =>
                    {
                        product.Categories = category;
                        product.Suppliers = supplier;
                        return product;
                    },
                    parameters,
                    splitOn: "CategoryID, SupplierID"
                );

                return new
                {
                    data = res.AsList(),
                    totalPage = roundedTotalPage,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }


        public async Task<object> UpdateProduct(int id, ProductRequest product)
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
                    status = 200
                };
            }
            catch
            {
                throw;
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
