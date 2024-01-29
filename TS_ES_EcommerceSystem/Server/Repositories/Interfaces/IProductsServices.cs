using Models;

namespace Server.Repositories.Interfaces
{
    public interface IProductsServices
    {
        public Task<object> GetProducts(int page, int pageSize, string productName);
        public Task<object> GetProduct(int id);
        public Task<object> GetProductEdit(int id);
        public Task<object> DeleteProduct(int id);
        public Task<object> UpdateProduct(int id, Products product);
        public Task<object> AddProduct(Products product);
    }
}
