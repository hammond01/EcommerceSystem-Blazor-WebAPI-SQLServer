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
                    status = 200,
                    msg = "Get category success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get category: {ex.Message}");
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
                    status = 200,
                    msg = "Get categories success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get categories: {ex.Message}");
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
                    status = 0,
                    msg = "Update category success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in update category: {ex.Message}");
            }
        }

        public async Task<object> AddCategory(Categories category)
        {
            try
            {
                var query = Extension.GetInsertQuery("Categories", "CategoryID", "CategoryName", "Description", "Picture");
                var data = await Program.Sql.QuerySingleAsync<Categories>(query, category);
                var dataAfterAdd = await GetCategory(data.CategoryID);
                return new
                {
                    data = dataAfterAdd,
                    status = 200,
                    msg = "Add category success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in add category: {ex.Message}");
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
                    status = 200,
                    msg = $"Delete category with CategoryID {id} success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in delete category: {ex.Message}");
            }

        }
    }
}
