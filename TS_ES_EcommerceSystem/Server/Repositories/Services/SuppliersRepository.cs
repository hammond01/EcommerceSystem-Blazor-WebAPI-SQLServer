using Dapper;
using Models;
using Server.Helper;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class SuppliersRepository : ISuppliersServices
    {
        public async Task<object> GetSuppliers()
        {
            try
            {
                var query = @"SELECT * FROM Suppliers";
                var res = (await Program.Sql.QueryAsync<Suppliers>(query)).AsList();
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

        public async Task<object> GetSupplier(int id)
        {
            try
            {
                var query = @"SELECT * FROM Suppliers WHERE SupplierID = @id;";
                var res = await Program.Sql.QuerySingleAsync<Suppliers>(query, new { id });
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

        public async Task<object> UpdateSupplier(int id, Suppliers supplier)
        {
            try
            {
                var query = @"UPDATE Suppliers
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
                                                    Fax = @Fax,
                                                    HomePage = @HomePage
                                                WHERE
                                                    SupplierID = @SupplierID;";
                supplier.SupplierID = id;
                await Program.Sql.ExecuteAsync(query, supplier);
                return new
                {
                    data = supplier,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }
        public async Task<object> AddSupplier(Suppliers supplier)
        {
            try
            {
                var query = Extension.GetInsertQuery("Suppliers", "SupplierID", "CompanyName", "ContactName", "ContactTitle",
                                                        "Address", "City", "Region", "PostalCode", "Country",
                                                            "Phone", "Fax", "HomePage");
                var data = await Program.Sql.QuerySingleAsync<Suppliers>(query, supplier);
                supplier.SupplierID = data.SupplierID;
                return new
                {
                    data = supplier,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteSupplier(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Suppliers", "SupplierID", id);
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
