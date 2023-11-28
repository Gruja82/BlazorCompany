using BlazorCompany.Client.Services.Categories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorCompany.Client.ExtensionMethods
{
    public static class RegisterServices
    {
        public static void AddServicesToContainer(this WebAssemblyHostBuilder hostBuilder)
        {
            hostBuilder.Services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
