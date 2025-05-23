using Crosscutting.Validators.Auth.Register;
using Domain.Validadores;
using FluentValidation;
using MediatR;
using Papelaria.API.Middleware;

namespace Papelaria.API.Setups;

public static class ValidatorsSetup
{
    public static IServiceCollection AddValidatorsSetup(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationMiddleware<,>))
            .AddValidatorsFromAssemblyContaining<RegistroRequestDtoValidator>()
            .AddValidatorsFromAssemblyContaining<CriarProdutoCommandValidator>();
        
        return services;
    }
}