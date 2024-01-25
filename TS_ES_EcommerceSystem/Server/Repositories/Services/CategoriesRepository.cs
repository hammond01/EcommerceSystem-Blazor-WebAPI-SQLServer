using Models;
using Dapper;
using Server.Repositories.Interfaces;
using Server.Helper;

namespace ServerLibrary.Repositories.Services
{
    public class CategoriesRepository : ICategoriesServices
    {
        public async Task<object> GetCategory(int id)
        {
            try
            {
                var query = @"SELECT CategoryID, CategoryName, Description, Picture FROM Categories WHERE CategoryID = @id;";
                var res = await Program.Sql.QuerySingleAsync<Categories>(query, new { id });
                return new
                {
                    data = res,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }


        public async Task<object> GetCategories()
        {
            try
            {
                var query = @"SELECT CategoryID, CategoryName, Description, Picture FROM Categories";
                var res = (await Program.Sql.QueryAsync<Categories>(query)).AsList();
                return new
                {
                    data = res,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> UpdateCategory(int id, Categories category)
        {
            try
            {
                var query = @"UPDATE Categories SET CategoryName = @CategoryName, Description = @Description, Picture = @Picture WHERE CategoryID = @CategoryID;";
                category.CategoryID = id;
                await Program.Sql.ExecuteAsync(query, category);
                return new
                {
                    data = category,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> AddCategory(Categories category)
        {
            try
            {
                var query = Extension.GetInsertQuery("Categories", "CategoryID", "CategoryName", "Description", "Picture");
                var data = await Program.Sql.QuerySingleAsync<Categories>(query, category);
                category.CategoryID = data.CategoryID;
                return new
                {
                    data = category,
                    status = 200
                };
            }
            catch
            {
                throw;
            }

        }
        public async Task<object> DeleteCategory(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Categories", "CategoryID", id);
                await Program.Sql.ExecuteAsync(query);
                return new
                {
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
