using Bogus;
using Dapper;
using Models;
using System;
using System.Data;

namespace Server.SeedData
{
    public class ProductInitializer
    {
        public static void SeedProducts(IDbConnection connection)
        {
            var faker = new Faker<Products>()
            .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
            .RuleFor(p => p.SupplierID, f => f.Random.Number(1, 10))
            .RuleFor(p => p.CategoryID, f => f.Random.Number(1, 5))
            .RuleFor(p => p.QuantityPerUnit, f => f.Commerce.ProductMaterial())
            .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 100))
            .RuleFor(p => p.UnitsInStock, f => f.Random.Short(1, 100))
            .RuleFor(p => p.UnitsOnOrder, f => f.Random.Short(0, 50))
            .RuleFor(p => p.ReorderLevel, f => f.Random.Short(5, 20))
            .RuleFor(p => p.Discontinued, f => f.Random.Bool());

            var products = faker.Generate(913); // Tạo 10 bản ghi Product giả mạo

            connection.Execute(@"
            INSERT INTO Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
            VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)", products);
        }
    }
}
