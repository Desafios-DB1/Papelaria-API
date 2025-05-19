using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Produto;

public class RemoverProdutoCommand : IRequest
{
    public Guid Id { get; set; }
}

public class RemoverProdutoCommandHandler(IProdutoRepository repository) : IRequestHandler<RemoverProdutoCommand>
{
    public async Task Handle(RemoverProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var produto = await repository.ObterPorIdAsync(request.Id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Produto"));
        
        await repository.RemoverESalvarAsync(produto);
    }
}