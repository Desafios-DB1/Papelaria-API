using Domain.Interfaces;
using Infra.Queries;

namespace API.Setups;

public static class QueriesSetup
{
    public static IServiceCollection AddQueriesSetup(this IServiceCollection services)
    {
        services
            .AddScoped<IProdutoQuery, ProdutoQuery>()
            .AddScoped<ICategoriaQuery, CategoriaQuery>()
            .AddScoped<ILogQuery, LogQuery>();
        
        return services;
    }
}