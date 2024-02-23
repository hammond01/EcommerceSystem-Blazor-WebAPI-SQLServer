using API.Warehouse.Repositories.Interfaces;
using API.Warehouse.Repositories.Services;
using Heplers.LoggersConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
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
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference =new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
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
        // Add services to the container.
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<LoggingActionFilter>();
        });
        //Add dependecy Injection
        builder.Services.AddScoped<IWarehouseServices, WareHouseRepository>();
        builder.Services.AddScoped<IStockInboundServices, StockInboundRepository>();
        builder.Services.AddScoped<IStockOutboundServices, StockOutboundRepository>();
        builder.Services.AddScoped<IProductionBatchServices, ProductionBatchRepository>();
        builder.Services.AddScoped<IDetailWarehouseServices, DetailWarehouseRepository>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Add authentication
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