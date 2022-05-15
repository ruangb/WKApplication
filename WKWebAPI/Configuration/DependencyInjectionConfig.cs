using Microsoft.Extensions.DependencyInjection;
using WKData.Repositories;
using WKManager.Implementation;
using WKManager.Interfaces.Managers;
using WKManager.Interfaces.Repositories;

namespace WKWebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoManager, ProdutoManager>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaManager, CategoriaManager>();
        }
    }
}
