using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Interfaces
{
    public interface IDetailWarehouseServices
    {
        public Task<object> GetDetailWarehouses();
        public Task<object> GetDetailWarehouse(int id);
        public Task<object> DeleteDetailWarehouse(int id);
        public Task<object> UpdateDetailWarehouse(int id, DetailWarehouse wareHouse);
        public Task<object> AddDetailWarehouse(DetailWarehouse wareHouse);
    }
}
