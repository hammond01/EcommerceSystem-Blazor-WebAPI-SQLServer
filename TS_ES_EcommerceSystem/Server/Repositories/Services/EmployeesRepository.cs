using Dapper;
using Models;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class EmployeesRepository : IEmloyeesServices
    {
        public async Task<object> AddEmployee(Employees employees)
        {
            try
            {
                var query = Extension.GetInsertQuery("Employees", "EmployeeID", "LastName", "FirstName", "Title", "TitleOfCourtesy",
                                                    "BirthDate", "HireDate", "Address", "City", "Region", "PostalCode", "Country",
                                                    "HomePhone", "Extension", "Photo", "Notes", "ReportsTo", "PhotoPath");
                var data = await Program.Sql.QueryFirstOrDefaultAsync<int>(query, employees);
                employees.EmployeeID = data!;
                return new
                {
                    data = employees,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteEmployee(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Employees", "EmployeeID", id);
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

        public async Task<object> GetEmployee(int id)
        {
            try
            {
                var query = @"SELECT * FROM Employees WHERE EmployeeID = @id;";
                var res = await Program.Sql.QuerySingleAsync<Employees>(query, new { id });
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

        public async Task<object> GetEmployees(int page, int pageSize, string fullName)
        {
            try
            {
                var offset = (page - 1) * pageSize;

                var query = @"SELECT * FROM Employees WHERE 
                                                            (@fullName IS NULL OR 
                                                                (CONCAT(FirstName, ' ', LastName) LIKE '%' + @fullName + '%') OR
                                                                (CONCAT(LastName, ' ', FirstName) LIKE '%' + @fullName + '%'))
                                                            ORDER BY EmployeeID
                                                            OFFSET @offset ROWS
                                                            FETCH NEXT @pageSize ROWS ONLY;";

                var parameters = new { fullName, offset, pageSize };

                var res = (await Program.Sql.QueryAsync<Employees>(query, parameters)).AsList();

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


        public async Task<object> UpdateEmployee(int id, Employees employees)
        {
            try
            {
                string query = @"UPDATE Employees
                                            SET
                                                LastName = @LastName,
                                                FirstName = @FirstName,
                                                Title = @Title,
                                                TitleOfCourtesy = @TitleOfCourtesy,
                                                BirthDate = @BirthDate,
                                                HireDate = @HireDate,
                                                Address = @Address,
                                                City = @City,
                                                Region = @Region,
                                                PostalCode = @PostalCode,
                                                Country = @Country,
                                                HomePhone = @HomePhone,
                                                Extension = @Extension,
                                                Photo = @Photo,
                                                Notes = @Notes,
                                                ReportsTo = @ReportsTo,
                                                PhotoPath = @PhotoPath
                                            WHERE EmployeeID = @EmployeeID;";
                employees.EmployeeID = id;
                await Program.Sql.ExecuteAsync(query, employees);
                return new
                {
                    data = employees,
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
