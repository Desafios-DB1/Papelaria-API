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
    
    public Guid UsuarioId { get; private set; } // sera usado para o logger
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
                // adicionar logger aqui
                produto.QuantidadeEstoque.AdicionarEstoque(request.Quantidade);
                break;
            case TipoOperacao.Saida:
                // adicionar logger aqui
                produto.QuantidadeEstoque.RetirarEstoque(request.Quantidade);
                break;
            default:
                throw new ArgumentException("Tipo de operação inválido.");
        }

        await repository.AtualizarESalvarAsync(produto);

        return produto.MapToDto();
    }
}