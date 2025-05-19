using Crosscutting.Constantes;
using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.Log;

public class RegistrarLogDeProdutoCommand : IRequest
{
    public Guid ProdutoId { get; set; }
    public string UsuarioId { get; set; }
    
    public TipoOperacao TipoOperacao { get; set; }
    public int QuantidadeAnterior { get; set; }
    public int QuantidadeAtual { get; set; }
}

public class RegistrarLogDeProdutoCommandHandler(ILogProdutoRepository repository, IProdutoRepository produtoRepository) : IRequestHandler<RegistrarLogDeProdutoCommand>
{
    public async Task Handle(RegistrarLogDeProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var log = new LogProduto
        {
            ProdutoId = request.ProdutoId,
            UsuarioId = request.UsuarioId,
            TipoOperacao = request.TipoOperacao,
            QuantidadeAnterior = request.QuantidadeAnterior,
            QuantidadeAtual = request.QuantidadeAtual,
        };

        await repository.AdicionarESalvarAsync(log);
    }
}