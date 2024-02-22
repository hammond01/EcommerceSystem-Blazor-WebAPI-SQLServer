using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Interfaces
{
    public interface IStockOutboundServices
    {
        public Task<object> GetStockOutbounds();
        public Task<object> GetStockOutbound(int id);
        public Task<object> DeleteStockOutbound(int id);
        public Task<object> UpdateStockOutbound(int id, StockOutbound wareHouse);
        public Task<object> AddStockOutbound(StockOutbound wareHouse);
        public Task<object> GetInformationOutboundByWareHouseID(int id);

    }
}
