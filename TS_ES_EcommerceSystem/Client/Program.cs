using Blazored.LocalStorage;
using Client;
using Client.Helpers;
using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

class Program
{
    public static HttpClient httpClient = default!;
    public static HttpClient httpClient_server = default!;
    public static HttpClient httpClient_auth = default!;
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Services.AddBlazorBootstrap();
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7275/api/")
        };
        httpClient_auth = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7253")
        };
        httpClient_server = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7297/api/")
        };

        builder.Services.AddScoped(sp =>
        new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7297/api/")
        }
        );

        builder.Services.AddScoped<ProductServices>();
        builder.Services.AddScoped<CategoryServices>();
        builder.Services.AddScoped<SuppliersServices>();
        builder.Services.AddScoped<WarehouseServices>();
        builder.Services.AddScoped<ProductionBatchServices>();
        builder.Services.AddScoped<StockInBoundServices>();
        builder.Services.AddScoped<StockOutBoundServices>();
        builder.Services.AddScoped<DetailWarehouseServices>();
        builder.Services.AddScoped<UnitServices>();
        builder.Services.AddScoped<SweetAlertService>();
        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddScoped<AuthServices>();
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddSweetAlert2();
        builder.Services.AddRadzenComponents();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        await builder.Build().RunAsync();
    }
}