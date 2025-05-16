using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Produto;

public class RemoverProdutoCommand : IRequest
{
    public Guid id { get; set; }
}

public class RemoverProdutoCommandHandler(IProdutoRepository repository) : IRequestHandler<RemoverProdutoCommand>
{
    public async Task Handle(RemoverProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var entity = await repository.ObterPorIdAsync(request.id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Produto"));
        
        await repository.RemoverESalvarAsync(entity);
    }
}