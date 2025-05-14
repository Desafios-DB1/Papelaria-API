using Crosscutting.Exceptions;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Commands;

public class CriarProdutoCommandHandlerTest
{
    private readonly Mock<IProdutoRepository> _repositoryMock = new();
    private readonly CriarProdutoCommandHandler _commandHandler;

    public CriarProdutoCommandHandlerTest()
    {
        _commandHandler = new CriarProdutoCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handler_QuandoRequisicaoValida_DeveCriarProdutoERetornarId()
    {
        var idEsperado = Guid.NewGuid();
        var command = ProdutoBuilder.Novo()
            .ComId(idEsperado)
            .CriarProdutoCommand();

        _repositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Produto>()))
            .ReturnsAsync(idEsperado);
        
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        result.Should().Be(idEsperado);
    }
    
    [Fact]
    public async Task Handler_QuandoRequisicaoNula_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = () => _commandHandler.Handle(null, CancellationToken.None);

        await act.Should().ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("A requisição é inválida.");
    }

    [Fact]
    public async Task Handler_QuandoErroNoBanco_DeveLancarException()
    {
        var command = ProdutoBuilder.Novo().CriarProdutoCommand();
        
        _repositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Erro ao salvar no banco de dados."));
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao salvar no banco de dados.");
    }
}