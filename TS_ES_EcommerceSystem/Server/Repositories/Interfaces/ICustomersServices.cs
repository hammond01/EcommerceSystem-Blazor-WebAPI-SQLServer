using Models;

namespace Server.Repositories.Interfaces
{
    public interface ICustomersServices
    {
        public Task<object> GetCustomers(int page, int pageSize, string contactName);
        public Task<object> GetCustomer(string id);
        public Task<object> DeleteCustomer(string id);
        public Task<object> UpdateCustomer(string id, Customers customers);
        public Task<object> AddCustomer(Customers customers);
    }
}
