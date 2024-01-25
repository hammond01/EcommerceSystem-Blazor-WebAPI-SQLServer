using Models;

namespace Server.Repositories.Interfaces
{
    public interface IEmloyeesServices
    {
        public Task<object> GetEmployees(int page, int pageSize, string fullName);
        public Task<object> GetEmployee(int id);
        public Task<object> DeleteEmployee(int id);
        public Task<object> UpdateEmployee(int id, Employees employees);
        public Task<object> AddEmployee(Employees employees);
    }
}
