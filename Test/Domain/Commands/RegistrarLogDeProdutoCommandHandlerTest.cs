using Crosscutting.Enums;
using Domain.Commands.Log;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Test.Domain.Commands;

public class RegistrarLogDeProdutoCommandHandlerTest
{
    private readonly Mock<ILogProdutoRepository> _repository = new();
    private readonly Mock<IProdutoRepository> _produtoRepository = new();
    private readonly RegistrarLogDeProdutoCommandHandler _commandHandler;

    public RegistrarLogDeProdutoCommandHandlerTest()
    {
        _commandHandler = new RegistrarLogDeProdutoCommandHandler(_repository.Object, _produtoRepository.Object);
    }
    
    [Fact]
    public async Task Handler_QuandoLogValido_DeveRegistrarLog()
    {
        var produtoId = Guid.NewGuid();
        LogProduto? logCapturado = null;
        var command = new RegistrarLogDeProdutoCommand
        {
            ProdutoId = produtoId,
            UsuarioId = "usuario123",
            TipoOperacao = TipoOperacao.Entrada,
            QuantidadeAnterior = 5,
            QuantidadeAtual = 10
        };
        
        _repository.Setup(r => r.AdicionarESalvarAsync(It.IsAny<LogProduto>()))
            .Callback<LogProduto>(log => logCapturado = log)
            .ReturnsAsync(produtoId);
        
        await _commandHandler.Handle(command, CancellationToken.None);
        
        _repository.Verify(r => r.AdicionarESalvarAsync(It.IsAny<LogProduto>()), Times.Once);

        logCapturado.Should().NotBeNull();
        logCapturado!.ProdutoId.Should().Be(command.ProdutoId);
        logCapturado.UsuarioId.Should().Be(command.UsuarioId);
        logCapturado.TipoOperacao.Should().Be(command.TipoOperacao);
        logCapturado.QuantidadeAnterior.Should().Be(command.QuantidadeAnterior);
        logCapturado.QuantidadeAtual.Should().Be(command.QuantidadeAtual);
        logCapturado.DataCriacao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}