using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WKData;

namespace WKWebApi.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WKContext>(options => options.UseMySql(configuration.GetConnectionString("WKConnection"), builder =>
                builder.MigrationsAssembly("WKData")));
        }

        public static void UseDataBaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<WKContext>();

            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
