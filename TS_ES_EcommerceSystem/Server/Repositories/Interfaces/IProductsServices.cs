using Models;

namespace Server.Repositories.Interfaces
{
    public interface IProductsServices
    {
        public Task<object> GetProducts();
        public Task<object> GetProduct(int id);
        public Task<object> DeleteProduct(int id);
        public Task<object> UpdateProduct(int id, Products product);
        public Task<object> AddProduct(Products product);
    }
}
