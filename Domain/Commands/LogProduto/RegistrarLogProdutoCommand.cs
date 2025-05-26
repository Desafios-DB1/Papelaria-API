using Crosscutting.Constantes;
using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Domain.Commands.LogProduto;

public class RegistrarLogProdutoCommand : IRequest
{
    public Guid ProdutoId { get; set; }
    public string UsuarioId { get; set; }
    public TipoOperacao TipoOperacao { get; set; }
    public int QuantidadeAnterior { get; set; }
    public int QuantidadeAtual { get; set; }
}

public class RegistrarLogProdutoCommandHandler(ILogProdutoRepository repository) : IRequestHandler<RegistrarLogProdutoCommand>
{
    public async Task Handle(RegistrarLogProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequisicaoInvalida);
        
        var log = new Entities.LogProduto
        {
            ProdutoId = request.ProdutoId,
            UsuarioId = request.UsuarioId,
            TipoOperacao = request.TipoOperacao,
            QuantidadeAnterior = request.QuantidadeAnterior,
            QuantidadeAtual = request.QuantidadeAtual
        };

        await repository.AdicionarESalvarAsync(log);
    }
}