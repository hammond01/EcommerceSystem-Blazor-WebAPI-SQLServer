using Dapper;
using Models;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class OrdersRepository : IOrdersServices
    {
        public async Task<object> GetOrder(int id)
        {
            try
            {
                var query = @"
                                SELECT 
                                    *
                                FROM Orders o
                                WHERE o.OrderID = @id";

                var res = (await Program.Sql.QueryAsync<Orders>(
                    query, new { id }
                )).AsList();
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

        public async Task<object> GetOrders(int page, int pageSize)
        {
            try
            {
                var query = @"
                                SELECT 
                                    *
                                FROM Orders o
                                ORDER BY o.OrderID
                                OFFSET @Offset ROWS
                                FETCH NEXT @PageSize ROWS ONLY";

                var offset = (page - 1) * pageSize;

                var res = (await Program.Sql.QueryAsync<Orders>(
                    query, new { Offset = offset, PageSize = pageSize }
                )).AsList();
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
        public async Task<object> AddOrder(Orders orders)
        {
            try
            {
                var query = Extension.GetInsertQuery("Orders", "OrderID", "CustomerID", "EmployeeID", "OrderDate",
                    "RequiredDate", "ShippedDate", "ShipVia", "Freight", "ShipName", "ShipAddress", "ShipCity", "ShipRegion", "ShipPostalCode", "ShipCountry");

                var data = await Program.Sql.QuerySingleAsync<Orders>(query, orders);
                orders.OrderID = data.OrderID;
                return new
                {
                    data = orders,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteOrder(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Orders", "OrderID", id);
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

        public async Task<object> UpdateOrder(int id, Orders orders)
        {
            try
            {
                var query = @"
                                UPDATE Orders
                                SET
                                    CustomerID = @CustomerID,
                                    EmployeeID = @EmployeeID,
                                    OrderDate = @OrderDate,
                                    RequiredDate = @RequiredDate,
                                    ShippedDate = @ShippedDate,
                                    ShipVia = @ShipVia,
                                    Freight = @Freight,
                                    ShipName = @ShipName,
                                    ShipAddress = @ShipAddress,
                                    ShipCity = @ShipCity,
                                    ShipRegion = @ShipRegion,
                                    ShipPostalCode = @ShipPostalCode,
                                    ShipCountry = @ShipCountry
                                WHERE OrderID = @OrderID";
                orders.OrderID = id;
                await Program.Sql.ExecuteAsync(query, orders);
                return new
                {
                    data = orders,
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
