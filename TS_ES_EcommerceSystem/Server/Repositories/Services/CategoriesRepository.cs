using Models;
using Dapper;
using Server.Repositories.Interfaces;
using Server.Helper;

namespace ServerLibrary.Repositories.Services
{
    public class CategoriesRepository : ICategoriesServices
    {
        public async Task<Categories> GetCategory(int id)
        {
            try
            {
                var query = @"SELECT CategoryID, CategoryName, Description, Picture FROM Categories WHERE CategoryID = @CategoryId;";
                var parameters = new Categories
                {
                    CategoryID = id
                };
                var data = await Program.Sql.QuerySingleAsync<Categories>(query, parameters);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCategory: {ex.Message}");
            }
        }


        public async Task<List<Categories>> GetCategories()
        {
            try
            {
                var query = "SELECT CategoryID, CategoryName, Description, Picture FROM Categories";
                var data = await Program.Sql.QueryAsync<Categories>(query);
                return data.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCategories: {ex.Message}");
            }
        }

        public async Task UpdateCategory(int id, Categories category)
        {
            try
            {
                var query = @"UPDATE Categories SET CategoryName = @CategoryName, Description = @Description, Picture = @Picture WHERE CategoryID = @CategoryID;";
                var parameters = new Categories
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = category.Picture,
                    CategoryID = id
                };
                var data = await Program.Sql.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddCategory(Categories category)
        {
            try
            {
                var id = await Program.Sql.ExecuteAsync(Extension.GetInsertQuery("Categories", "CategoryID", "CategoryName", "Description", "Picture"), category);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddCategory: {ex.Message}");
            }

        }
        public async Task DeleteCategory(int id)
        {
            try
            {
                await Program.Sql.ExecuteAsync(Extension.GetDeleteQueryInt("Categories", "CategoryID", id));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeleteCategory: {ex.Message}");
            }

        }
    }
}
