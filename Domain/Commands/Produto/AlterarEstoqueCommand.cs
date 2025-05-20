using Crosscutting.Constantes;
using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Mappers;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Produto;

public class AlterarEstoqueCommand : IRequest<ProdutoDto>
{
    public Guid ProdutoId { get; set; }
    public TipoOperacao TipoOperacao { get; set; }
    public int Quantidade { get; set; }
    
    public Guid UsuarioId { get; private set; } //TODO: utilizar no log
}

public class AlterarEstoqueCommandHandler(IProdutoRepository repository) : IRequestHandler<AlterarEstoqueCommand, ProdutoDto>
{
    public async Task<ProdutoDto> Handle(AlterarEstoqueCommand request, CancellationToken cancellationToken)
    {
        var produto = await repository.ObterPorIdAsync(request.ProdutoId)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Produto"));
        
        switch (request.TipoOperacao)
        {
            case TipoOperacao.Entrada:
                produto.QuantidadeEstoque.AdicionarEstoque(request.Quantidade);
                break;
            case TipoOperacao.Saida:
                produto.QuantidadeEstoque.RetirarEstoque(request.Quantidade);
                break;
            default:
                throw new ArgumentException("Tipo de operação inválido.");
        }
        
        //TODO: Implementar log de alteração de estoque

        await repository.AtualizarESalvarAsync(produto);

        return produto.MapToDto();
    }
}