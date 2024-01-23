using Dapper;
using Models;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class CustomersRepository : ICustomersServices
    {
        public async Task<object> AddCustomer(Customers customers)
        {
            try
            {
                var query = Extension.GetInsertQuery("Customers", "CustomerID", "CustomerID", "CompanyName", "ContactName", "ContactTitle",
                                                    "Address", "City", "Region", "PostalCode", "Country", "Phone", "Fax");
                var data = await Program.Sql.QueryFirstOrDefaultAsync<string>(query, customers);
                customers.CustomerID = data!;
                return new
                {
                    data = customers,
                    status = 200,
                    msg = "Add customer success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in add customer: {ex.Message}");
            }
        }

        public async Task<object> DeleteCustomer(string id)
        {
            try
            {
                var query = Extension.GetDeleteQueryString("Customers", "CustomerID", id);
                await Program.Sql.ExecuteAsync(query);
                return new
                {
                    status = 200,
                    msg = $"Delete customer with CustomerID {id} success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in delete customer: {ex.Message}");
            }
        }

        public async Task<object> GetCustomer(string id)
        {
            try
            {
                var query = @"SELECT * FROM Customers WHERE CustomerID = @id;";
                var res = await Program.Sql.QuerySingleAsync<Categories>(query, new { id });
                return new
                {
                    data = res,
                    status = 200,
                    msg = "Get customer success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get customer: {ex.Message}");
            }
        }

        public async Task<object> GetCustomers()
        {
            try
            {
                var query = @"SELECT * FROM Customers";
                var res = (await Program.Sql.QueryAsync<Customers>(query)).AsList();
                return new
                {
                    data = res,
                    status = 200,
                    msg = "Get customers success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in get customers: {ex.Message}");
            }
        }

        public async Task<object> UpdateCustomer(string id, Customers customers)
        {
            try
            {
                string query = @"
                                UPDATE Customers
                                SET
                                    CompanyName = @CompanyName,
                                    ContactName = @ContactName,
                                    ContactTitle = @ContactTitle,
                                    Address = @Address,
                                    City = @City,
                                    Region = @Region,
                                    PostalCode = @PostalCode,
                                    Country = @Country,
                                    Phone = @Phone,
                                    Fax = @Fax
                                WHERE CustomerID = @CustomerID";
                customers.CustomerID = id;
                await Program.Sql.ExecuteAsync(query, customers);
                return new
                {
                    data = customers,
                    status = 0,
                    msg = "Update customer success!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in update customer: {ex.Message}");
            }
        }
    }
}
