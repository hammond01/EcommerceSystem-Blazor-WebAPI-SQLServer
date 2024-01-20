using Models;

namespace ServerLibrary.Repositories.Interfaces
{
    public interface ICategoriesServices
    {
        List<Categories> GetCategorys();
        Categories GetCategory(int id);
        void DeleteCategory(int id);
        void UpdateCategory(int id, Categories category);
        void AddCategory(Categories category);

    }
}
