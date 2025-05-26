using Crosscutting.Constantes;
using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Commands.LogProduto;
using Domain.Mappers;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Produto;

public class AlterarEstoqueCommand : IRequest<ProdutoDto>
{
    public Guid ProdutoId { get; set; }
    public TipoOperacao TipoOperacao { get; set; }
    public int Quantidade { get; set; }
    
    public string UsuarioId { get; private set; }
    
    public void PreencherUsuarioId(string usuarioId)
    {
        UsuarioId = usuarioId;
    }
}

public class AlterarEstoqueCommandHandler(IProdutoRepository repository, IMediator mediator) : IRequestHandler<AlterarEstoqueCommand, ProdutoDto>
{
    public async Task<ProdutoDto> Handle(AlterarEstoqueCommand request, CancellationToken cancellationToken)
    {
        var produto = await repository.ObterPorIdAsync(request.ProdutoId)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Produto"));
        
        var quantidadeAnterior = produto.QuantidadeEstoque.QuantidadeAtual;
        
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
        
        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = produto.Id,
            UsuarioId = request.UsuarioId,
            TipoOperacao = request.TipoOperacao,
            QuantidadeAnterior = quantidadeAnterior,
            QuantidadeAtual = produto.QuantidadeEstoque.QuantidadeAtual
        };

        await mediator.Send(command, cancellationToken);

        await repository.AtualizarESalvarAsync(produto);

        return produto.MapToDto();
    }
}