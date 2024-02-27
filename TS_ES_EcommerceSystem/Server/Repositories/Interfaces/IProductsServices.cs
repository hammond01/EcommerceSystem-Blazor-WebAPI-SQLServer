using Models;
using Models.RequestModel;

namespace Server.Repositories.Interfaces
{
    public interface IProductsServices
    {
        public Task<object> GetProducts(int page, int pageSize, string productName);
        public Task<object> GetAllProducts();
        public Task<object> GetProductContinued(int page, int pageSize, string productName);
        public Task<object> GetProductDisContinued(int page, int pageSize, string productName);
        public Task<object> GetProduct(int id);
        public Task<object> GetProductEdit(int id);
        public Task<object> DeleteProduct(int id, bool reIntroduce);
        public Task<object> UpdateProduct(int id, ProductRequest product);
        public Task<object> AddProduct(ProductRequest product);
    }
}
