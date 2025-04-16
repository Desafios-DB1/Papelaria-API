using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Papelaria.API.Setups;

public static class SwaggerSetup
{
    public static void ConfigureSwagger(SwaggerGenOptions c)
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Papelaria API",
            Description = "Documentação sobre consumo da api do estoque da papelaria"
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insira o token JWT no formato: Bearer {seu token}",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    { 
                        Type = ReferenceType.SecurityScheme, 
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); 
        c.IncludeXmlComments(xmlPath);
        /*c.ExampleFilters();*/
    }
}