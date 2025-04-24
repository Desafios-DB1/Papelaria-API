using Crosscutting.Validators.Auth.Register;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Papelaria.API.Setups;

public static class ValidatorsSetup
{
    public static IServiceCollection AddValidatorsSetup(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
        return services;
    }
}