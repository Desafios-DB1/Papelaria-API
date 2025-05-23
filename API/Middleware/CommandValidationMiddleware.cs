using Crosscutting.Exceptions;
using FluentValidation;
using MediatR;

namespace Papelaria.API.Middleware;

public class CommandValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public CommandValidationMiddleware(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var errors = failures.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (errors.Any())
            throw new RegraDeNegocioException(errors.Select(e => e.ErrorMessage).ToList());

        return await next();
    }
}