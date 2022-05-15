using LC.Manager.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace WKWebApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(NovoProdutoMappingProfile), typeof(AtualizaProdutoMappingProfile));
        }
    }
}
