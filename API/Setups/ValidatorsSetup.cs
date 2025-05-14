using Crosscutting.Validators.Auth.Register;
using Domain.Validadores;
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
            .AddValidatorsFromAssemblyContaining<RegistroRequestDtoValidator>()
            .AddValidatorsFromAssemblyContaining<CriarProdutoCommandValidator>();
        
        return services;
    }
}