using Models;

namespace Server.Repositories.Interfaces
{
    public interface IShippersServices
    {
        public Task<object> GetShippers();
        public Task<object> GetShipper(int id);
        public Task<object> DeleteShipper(int id);
        public Task<object> UpdateShipper(int id, Shippers shippers);
        public Task<object> AddShipper(Shippers shippers);
    }
}
