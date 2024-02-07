using Dapper;
using Heplers;
using Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Services
{
    public class ShippersRepository : IShippersServices
    {
        public async Task<object> AddShipper(Shippers shippers)
        {
            try
            {
                var query = Extension.GetInsertQuery("Shippers", "ShipperID", "CompanyName", "Phone");

                var data = await Program.Sql.QueryFirstOrDefaultAsync<int>(query, shippers);
                shippers.ShipperID = data;
                return new
                {
                    data = shippers,
                    status = 200
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<object> DeleteShipper(int id)
        {
            try
            {
                var query = Extension.GetDeleteQueryInt("Shippers", "ShipperID", id);
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

        public async Task<object> GetShipper(int id)
        {
            try
            {
                var query = @"SELECT * FROM Shippers WHERE ShipperID = @id;";
                var res = await Program.Sql.QuerySingleAsync<Shippers>(query, new { id });
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

        public async Task<object> GetShippers()
        {
            try
            {
                var query = @"SELECT * FROM Shippers;";
                var res = (await Program.Sql.QueryAsync<Shippers>(query)).AsList();
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

        public async Task<object> UpdateShipper(int id, Shippers shippers)
        {
            try
            {
                string query = @"
                                UPDATE Shippers SET CompanyName = @CompanyName, Phone = @Phone WHERE ShipperID = @ShipperID";
                shippers.ShipperID = id;
                await Program.Sql.ExecuteAsync(query, shippers);
                return new
                {
                    data = shippers,
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
