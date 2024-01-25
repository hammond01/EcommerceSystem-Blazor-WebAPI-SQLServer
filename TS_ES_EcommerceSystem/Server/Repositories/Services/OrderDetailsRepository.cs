using Dapper;
using Models;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class OrderDetailsRepository : IOrderDetailsServices
    {
        public async Task<object> AddOrderDetail(OrderDetails orderDetails)
        {
            try
            {
                var query = Extension.GetInsertQuery("OrderDetails", "OrderID", "OrderID", "ProductID", "UnitPrice", "Quantity", "Discount");

                var data = await Program.Sql.QuerySingleAsync<OrderDetails>(query, orderDetails);
                orderDetails.OrderID = data.OrderID;
                return new
                {
                    data = orderDetails,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteOrderDetail(int orderID, int productID)
        {
            try
            {
                var query = @"DELETE FROM OrderDetails WHERE OrderID = @OrderID AND ProductID = @ProductID";
                await Program.Sql.ExecuteAsync(query, new OrderDetails
                {
                    OrderID = orderID,
                    ProductID = productID
                });
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

        public async Task<object> GetOrderDetail(int id)
        {
            try
            {
                var query = @"SELECT * FROM OrderDetails o LEFT JOIN Products p ON o.ProductID = p.ProductID WHERE o.OrderID = @id";

                var res = (await Program.Sql.QueryAsync<OrderDetails, Products, OrderDetails>(query,
                    (orderDetails, product) =>
                    {
                        orderDetails.Products = product;
                        return orderDetails;
                    },
                    new { id },
                    splitOn: "ProductID"
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

        public async Task<object> GetOrderDetails()
        {
            try
            {
                var query = @"SELECT * FROM OrderDetails o LEFT JOIN Products p ON o.ProductID = p.ProductID";

                var res = (await Program.Sql.QueryAsync<OrderDetails, Products, OrderDetails>(query,
                    (orderDetails, product) =>
                    {
                        orderDetails.Products = product;
                        return orderDetails;
                    },
                    splitOn: "ProductID"
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

        public async Task<object> UpdateOrderDetail(int id, OrderDetails orderDetails)
        {
            try
            {
                var query = @"
                                UPDATE OrderDetails
                                SET
                                    UnitPrice = @UnitPrice,
                                    Quantity = @Quantity,
                                    Discount = @Discount
                                WHERE OrderID = @OrderID AND ProductID = @ProductID";
                orderDetails.OrderID = id;
                await Program.Sql.ExecuteAsync(query, orderDetails);
                return new
                {
                    data = orderDetails,
                    status = 0
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
