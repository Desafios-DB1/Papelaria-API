using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Domain.Commands.Categoria;

public class RemoverCategoriaCommand : IRequest
{
    public Guid CategoriaId { get; set; }
}

public class RemoverCategoriaCommandHandler(
    ICategoriaRepository repository, 
    IValidator<RemoverCategoriaCommand> validator) 
    : IRequestHandler<RemoverCategoriaCommand>
{
    public async Task Handle(RemoverCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var categoria = await repository.ObterPorIdAsync(request.CategoriaId) 
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new RegraDeNegocioException(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        
        
        await repository.RemoverESalvarAsync(categoria);
    }
}