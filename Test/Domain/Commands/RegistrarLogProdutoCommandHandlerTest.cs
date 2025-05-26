using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Commands.LogProduto;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Test.Domain.Commands;

public class RegistrarLogProdutoCommandHandlerTest
{
    private readonly Mock<ILogProdutoRepository> _repository = new();
    private readonly RegistrarLogProdutoCommandHandler _handler;

    public RegistrarLogProdutoCommandHandlerTest()
    {
        _handler = new RegistrarLogProdutoCommandHandler(_repository.Object);
    }
    
    [Fact]
    public async Task Handler_QuandoAlteracaoDeEstoqueValida_DeveRegistrarLog()
    {
        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = Guid.NewGuid(),
            TipoOperacao = TipoOperacao.Entrada,
            UsuarioId = Guid.NewGuid().ToString(),
            QuantidadeAnterior = 10,
            QuantidadeAtual = 20
        };
        
        await _handler.Handle(command, CancellationToken.None);
        
        _repository.Verify(r => r.AdicionarESalvarAsync(It.IsAny<LogProduto>()), Times.Once);
    }
    
    [Fact]
    public async Task Handler_QuandoRequisicaoNula_DeveLancarRequisicaoInvalidaException()
    {
        RegistrarLogProdutoCommand command = null;
        
        Func<Task> act = () => _handler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("A requisição é inválida.");
    }
}