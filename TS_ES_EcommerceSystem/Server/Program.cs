using Server.Helper.JWTModel;
using Server.Repositories.Interfaces;
using Server.Repositories.Services;
using ServerLibrary.Repositories.Services;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

class Program
{
    public static SqlConnection Sql { get; private set; } = default!;
    public static IConfiguration Config { get; private set; } = default!;

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add Cors ALL Option
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Add services to the container.

        builder.Services.AddControllers()
                        .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        //config JWT credentials
        builder.Services.Configure<Appsettings>(builder.Configuration.GetSection("Appsettings"));
        var secretKey = builder.Configuration["Appsettings:SecretKey"]!;
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

        //Add JWT
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(h =>
            {
                h.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        builder.Services.AddScoped<ICategoriesServices, CategoriesRepository>();
        builder.Services.AddScoped<IProductsServices, ProductsRepository>();
        builder.Services.AddScoped<ISuppliersServices, SuppliersRepository>();
        builder.Services.AddScoped<IOrdersServices, OrdersRepository>();
        builder.Services.AddScoped<IOrderDetailsServices, OrderDetailsRepository>();
        builder.Services.AddScoped<ICustomersServices, CustomersRepository>();
        builder.Services.AddScoped<IShippersServices, ShippersRepository>();
        builder.Services.AddScoped<IEmloyeesServices, EmployeesRepository>();
        builder.Services.AddScoped<IAccountRoleServices, AccountRoleRepository>();
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

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();
    }
}