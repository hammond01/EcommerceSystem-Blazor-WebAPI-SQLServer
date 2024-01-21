using Models;

namespace Server.Repositories.Interfaces
{
    public interface ICategoriesServices
    {
        public Task<List<Categories>> GetCategories();
        public Task<Categories> GetCategory(int id);
        public Task DeleteCategory(int id);
        public Task UpdateCategory(int id, Categories category);
        public Task<int> AddCategory(Categories category);

    }
}
