using Models;

namespace Server.Repositories.Interfaces
{
    public interface IOrdersServices
    {
        public Task<object> GetOrders(int page, int pageSize);
        public Task<object> GetOrder(int id);
        public Task<object> DeleteOrder(int id);
        public Task<object> UpdateOrder(int id, Orders orders);
        public Task<object> AddOrder(Orders orders);
    }
}
