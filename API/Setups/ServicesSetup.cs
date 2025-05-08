using Domain.Interfaces;
using Domain.Services;

namespace API.Setups;

public static class ServicesSetup
{
    public static IServiceCollection AddServicesSetup(this IServiceCollection services)
    {
        services
            .AddScoped<ICategoriaService, CategoriaService>();
        
        return services;
    }
}