using Models;

namespace Server.Repositories.Interfaces
{
    public interface ICustomerServices
    {
        public Task<object> GetCustomers();
        public Task<object> GetCustomer(string id);
        public Task<object> DeleteCustomer(string id);
        public Task<object> UpdateCustomer(string id, Customers customers);
        public Task<object> AddCustomer(Customers customers);
    }
}
