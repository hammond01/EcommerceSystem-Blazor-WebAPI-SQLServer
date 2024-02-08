using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Interfaces
{
    public interface IStockInboundServices
    {
        public Task<object> GetStockInbounds();
        public Task<object> GetStockInbound(int id);
        public Task<object> DeleteStockInbound(int id);
        public Task<object> UpdateStockInbound(int id, StockInbound wareHouse);
        public Task<object> AddStockInbound(StockInbound wareHouse);
    }
}
