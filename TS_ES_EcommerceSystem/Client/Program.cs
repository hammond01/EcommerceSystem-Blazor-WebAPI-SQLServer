using Client;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

class Program
{
    public static HttpClient httpClient = default!;
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddScoped(sp =>
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7297/api/")
        }
        );

        builder.Services.AddScoped(sp =>
        new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7297/api/")
        }
        );

        builder.Services.AddScoped<ProductServices>();
        builder.Services.AddScoped<CategoryServices>();
        builder.Services.AddScoped<SuppliersServices>();
        builder.Services.AddBlazorBootstrap();
        await builder.Build().RunAsync();
    }
}