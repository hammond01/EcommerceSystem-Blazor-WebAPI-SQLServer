using AuthenticationAPI.Data;
using AuthenticationAPI.Models;
using AuthenticationAPI.Repositories.Interfaces;
using AuthenticationAPI.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication API", Version = "v1" });
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
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

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
//builder.Services.AddDbContext<ApplicationDBContext>(options =>
//{
//    options.UseInMemoryDatabase("MemoryDatabase");
//});

/*builder.Services.AddDbContext<ApplicationDBContext>(h =>
{
    h.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});*/
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

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"))
        .EnableSensitiveDataLogging(true));

builder.Services.AddScoped<IAccountServices, AccountRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});


var app = builder.Build();


//Seed data Roles
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    RoleInitializer.InitializeAsync(roleManager).GetAwaiter().GetResult();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}


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
