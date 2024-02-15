using Models.WarehouseModel;

namespace API.Warehouse.Repositories.Interfaces
{
    public interface IProductionBatchServices
    {
        public Task<object> GetProductionBatchs();
        public Task<object> GetProductionBatch(int id);
        public Task<object> DeleteProductionBatch(int id);
        public Task<object> UpdateProductionBatch(int id, ProductionBatch wareHouse);
        public Task<object> AddProductionBatch(ProductionBatch wareHouse);
    }
}
