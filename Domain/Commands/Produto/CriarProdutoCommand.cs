using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Mappers;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Produto;

public class CriarProdutoCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeMinima { get; set; }
    public int QuantidadeAtual { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; }
}

public class CriarProdutoCommandHandler(IProdutoRepository repository) : IRequestHandler<CriarProdutoCommand, Guid>
{
    public async Task<Guid> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var entity = request.MapToEntity();
        
        return await repository.AdicionarESalvarAsync(entity);
    }
}