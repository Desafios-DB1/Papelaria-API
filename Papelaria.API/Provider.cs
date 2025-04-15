using Microsoft.AspNetCore.Authentication.JwtBearer;
using Papelaria.API.Setups;

namespace Papelaria.API;

public static class Provider
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidatorsSetup();
        
        services.AddSwaggerGen(SwaggerSetup.ConfigureSwagger);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:5127";
                options.Audience = "papelariaapi";
                options.RequireHttpsMetadata = false;
            });

        services.AddAuthorization();
    }
}