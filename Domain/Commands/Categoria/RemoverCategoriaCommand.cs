using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Categoria;

public class RemoverCategoriaCommand : IRequest
{
    public Guid CategoriaId { get; set; }
}

public class RemoverCategoriaCommandHandler(ICategoriaRepository repository, ICategoriaService service) : IRequestHandler<RemoverCategoriaCommand>
{
    public async Task Handle(RemoverCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var categoria = await repository.ObterPorIdAsync(request.CategoriaId) 
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));
        
        if (!await service.PodeRemoverCategoria(request.CategoriaId))
            throw new RegraDeNegocioException(ErrorMessages.CategoriaPossuiProdutos);
        
        await repository.RemoverESalvarAsync(categoria);
    }
}