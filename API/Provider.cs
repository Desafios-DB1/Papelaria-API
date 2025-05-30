using System.Text;
using API.Exemplos;
using API.Setups;
using Domain.Commands.Produto;
using Domain.Entities;
using Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Papelaria.API.Setups;
using Swashbuckle.AspNetCore.Filters;

namespace API;

public static class Provider
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositoriesSetup()
            .AddQueriesSetup()
            .AddValidatorsSetup();
        
        services.AddDbContextSetup(configuration);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<CriarProdutoCommandHandler>());
        
        services.AddSwaggerGen(SwaggerSetup.ConfigureSwagger);
        services.AddSwaggerExamplesFromAssemblyOf<LoginRequestDtoExample>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:ValidAudience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret", "Jwt:Secret configuration value is missing")))
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });
        
        services.AddIdentityCore<Usuario>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorization();
    }
}