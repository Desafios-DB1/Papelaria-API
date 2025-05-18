using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Mappers;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Categoria;

public class CriarCategoriaCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
}

public class CriarCategoriaCommandHandler(ICategoriaRepository repository) : IRequestHandler<CriarCategoriaCommand, Guid>
{
    public async Task<Guid> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);

        var categoria = request.MapToEntity();

        return await repository.AdicionarESalvarAsync(categoria);
    }
}