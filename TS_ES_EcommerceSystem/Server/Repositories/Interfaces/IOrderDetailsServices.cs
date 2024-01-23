using Models;

namespace Server.Repositories.Interfaces
{
    public interface IOrderDetailsServices
    {
        public Task<object> GetOrderDetails();
        public Task<object> GetOrderDetail(int id);
        public Task<object> DeleteOrderDetail(int orderID, int productID);
        public Task<object> UpdateOrderDetail(int id, OrderDetails orderDetails);
        public Task<object> AddOrderDetail(OrderDetails orderDetails);
    }
}
