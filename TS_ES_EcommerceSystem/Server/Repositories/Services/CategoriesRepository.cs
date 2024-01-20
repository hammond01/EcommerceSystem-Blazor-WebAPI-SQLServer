
using Models;
using Dapper;
using Server.Repositories.Interfaces;

namespace ServerLibrary.Repositories.Services
{
    public class CategoriesRepository : ICategoriesServices
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
                var data = Program.Sql.QuerySingle<Categories>("Select" +
                    " c.CategoryID, c.CategoryName, c.Description, c.Picture from Categories c " +
                    "where c.CategoryID = @CategoryID",
                    new Categories
                    {
                        CategoryID = id,
                    });
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public List<Categories> GetCategorys()
        {
            try
            {
                var data = Program.Sql.Query<Categories>("Select c.CategoryID, c.CategoryName, c.Description, c.Picture from Categories c").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateCategory(int id, Categories category)
        {
            throw new NotImplementedException();
        }
    }
}
