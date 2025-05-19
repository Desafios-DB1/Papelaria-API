using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Produto;

public class AtualizarProdutoCommand : IRequest<Guid>, IAtualizarCommand
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeMinima { get; set; }
    public int QuantidadeAtual { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; }
}

public class AtualizarProdutoCommandHandler(IProdutoRepository repository) : IRequestHandler<AtualizarProdutoCommand, Guid>
{
    public async Task<Guid> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var produtoAntigo = await repository.ObterPorIdAsync(request.Id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Produto"));
        
        produtoAntigo.Atualizar(request);

        return await repository.AtualizarESalvarAsync(produtoAntigo);
    }
}