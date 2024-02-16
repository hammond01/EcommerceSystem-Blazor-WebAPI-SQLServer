using Client;
using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

class Program
{
    public static HttpClient httpClient = default!;
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
        builder.Services.AddScoped<SweetAlertService>();
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddSweetAlert2();
        builder.Services.AddRadzenComponents();
        await builder.Build().RunAsync();
    }
}