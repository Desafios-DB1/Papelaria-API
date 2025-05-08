using Infra;
using Microsoft.EntityFrameworkCore;

namespace API.Setups;

public static class DbContextSetup
{
    public static void AddDbContextSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
