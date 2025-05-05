using Domain.Repositories;
using Infra.Repositories;

namespace API.Setups;

public static class RepositoriesSetup
{
    public static IServiceCollection AddRepositoriesSetup(this IServiceCollection services)
    {
        services
            .AddScoped<IProdutoRepository, ProdutoRepository>()
            .AddScoped<ICategoriaRepository, CategoriaRepository>()
            .AddScoped<ILogProdutoRepository, LogProdutoRepository>();
        
        return services;
    }
}