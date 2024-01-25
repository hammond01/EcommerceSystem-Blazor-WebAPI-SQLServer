using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Server.Helper.LoggersConfig;
using Server.Repositories.Interfaces;
using Server.Repositories.Services;
using ServerLibrary.Repositories.Services;
using System.Data.SqlClient;
using System.Text;

class Program
{
    public static SqlConnection Sql { get; private set; } = default!;
    public static IConfiguration Config { get; private set; } = default!;

    private static void Main(string[] args)
    {



        var builder = WebApplication.CreateBuilder(args);
        // Config Serilog Logger
        Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.Console()
                            .WriteTo.File("Helper/Log/Logger.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
        builder.Host.UseSerilog();

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



        //Add Dependency Injection
        builder.Services.AddScoped<ICategoriesServices, CategoriesRepository>();
        builder.Services.AddScoped<IProductsServices, ProductsRepository>();
        builder.Services.AddScoped<ISuppliersServices, SuppliersRepository>();
        builder.Services.AddScoped<IOrdersServices, OrdersRepository>();
        builder.Services.AddScoped<IOrderDetailsServices, OrderDetailsRepository>();
        builder.Services.AddScoped<ICustomersServices, CustomersRepository>();
        builder.Services.AddScoped<IShippersServices, ShippersRepository>();
        builder.Services.AddScoped<IEmloyeesServices, EmployeesRepository>();

        // Add services to the container.
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<LoggingActionFilter>();
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(h =>
        {
            h.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            h.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            h.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(h =>
        {
            h.SaveToken = true;
            h.RequireHttpsMetadata = false;
            h.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
            };
        });

        //Register ILogger and LoggerFactory
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
        });

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