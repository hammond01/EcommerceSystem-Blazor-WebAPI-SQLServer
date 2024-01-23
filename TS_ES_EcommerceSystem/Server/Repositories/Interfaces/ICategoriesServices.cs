using Models;

namespace Server.Repositories.Interfaces
{
    public interface ICategoriesServices
    {
        public Task<object> GetCategories();
        public Task<object> GetCategory(int id);
        public Task<object> DeleteCategory(int id);
        public Task<object> UpdateCategory(int id, Categories category);
        public Task<object> AddCategory(Categories category);
    }
}
