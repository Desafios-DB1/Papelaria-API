using System.Text.Json.Serialization;
using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Categoria;

public class AtualizarCategoriaCommand : IRequest<Guid>, IAtualizarCommand
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
}

public class AtualizarCategoriaCommandHandler(ICategoriaRepository repository) : IRequestHandler<AtualizarCategoriaCommand, Guid>
{
    public async Task<Guid> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var categoriaAntiga = await repository.ObterPorIdAsync(request.Id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Categoria"));
        
        categoriaAntiga.Atualizar(request);
        
        return await repository.AtualizarESalvarAsync(categoriaAntiga);
    }
}