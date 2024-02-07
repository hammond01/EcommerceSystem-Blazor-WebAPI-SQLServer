using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Interfaces
{
    public interface IWarehouseServices
    {
        public Task<object> GetWareHouses();
        public Task<object> GetWareHouse(int id);
        public Task<object> DeleteWareHouse(int id);
        public Task<object> UpdateWareHouse(int id, WareHouse wareHouse);
        public Task<object> AddWareHouse(WareHouse wareHouse);
    }
}
