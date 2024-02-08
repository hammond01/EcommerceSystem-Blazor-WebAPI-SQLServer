using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Interfaces
{
    public interface IProductionBatchServices
    {
        public Task<object> GetProductionBatchs();
        public Task<object> GetProductionBatch(string id);
        public Task<object> DeleteProductionBatch(string id);
        public Task<object> UpdateProductionBatch(string id, ProductionBatch wareHouse);
        public Task<object> AddProductionBatch(ProductionBatch wareHouse);
    }
}
