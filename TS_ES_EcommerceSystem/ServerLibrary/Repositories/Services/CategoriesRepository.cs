
using Models;
using ServerLibrary.Helper;
using ServerLibrary.Repositories.Interfaces;

namespace ServerLibrary.Repositories.Services
{
    public class CategoriesRepository(DBConnect _sql) : ICategoriesServices
    {
        public void AddCategory(Categories category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Categories GetCategory(int id)
        {
            try
            {
                var data = _sql.
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<Categories> GetCategorys()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(int id, Categories category)
        {
            throw new NotImplementedException();
        }
    }
}
