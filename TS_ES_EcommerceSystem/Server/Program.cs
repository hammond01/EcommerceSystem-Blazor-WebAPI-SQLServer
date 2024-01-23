using Server.Repositories.Interfaces;
using Server.Repositories.Services;
using ServerLibrary.Repositories.Services;
using System.Data.SqlClient;

class Program
{
    public static SqlConnection Sql { get; private set; } = default!;
    public static IConfiguration Config { get; private set; } = default!;

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ICategoriesServices, CategoriesRepository>();
        builder.Services.AddScoped<IProductsServices, ProductsRepository>();
        builder.Services.AddScoped<ISuppliersServices, SuppliersRepository>();
        builder.Services.AddScoped<IOrdersServices, OrdersRepository>();
        builder.Services.AddScoped<IOrderDetailsServices, OrderDetailsRepository>();
        builder.Services.AddScoped<ICustomerServices, CustomerRepository>();
        builder.Services.AddScoped<IShippersServices, ShippersRepository>();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        Config = app.Configuration;
        Sql = new SqlConnection(Config["SQL"]);
        Sql.Open();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}