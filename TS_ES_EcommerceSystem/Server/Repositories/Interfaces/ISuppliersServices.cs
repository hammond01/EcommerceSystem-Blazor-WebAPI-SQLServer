using Models;

namespace Server.Repositories.Interfaces
{
    public interface ISuppliersServices
    {
        public Task<object> GetSuppliers();
        public Task<object> GetSupplier(int id);
        public Task<object> DeleteSupplier(int id);
        public Task<object> UpdateSupplier(int id, Suppliers suppliers);
        public Task<object> AddSupplier(Suppliers suppliers);
    }
}
