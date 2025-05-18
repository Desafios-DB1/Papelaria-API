using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Commands;

public class CriarCategoriaCommandHandlerTest
{
    private readonly Mock<ICategoriaRepository> _repositoryMock = new();
    private readonly CriarCategoriaCommandHandler _commandHandler;

    public CriarCategoriaCommandHandlerTest()
    {
        _commandHandler = new CriarCategoriaCommandHandler(_repositoryMock.Object);
    }
    
    [Fact]
    public async Task Handler_QuandoRequisicaoValida_DeveCriarCategoriaERetornarId()
    {
        var idEsperado = Guid.NewGuid();
        var command = CategoriaBuilder.Novo()
            .ComId(idEsperado)
            .CriarCategoriaCommand();

        _repositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Categoria>()))
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
        var command = CategoriaBuilder.Novo().CriarCategoriaCommand();
        
        _repositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Categoria>()))
            .ThrowsAsync(new Exception("Erro ao salvar no banco de dados."));
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao salvar no banco de dados.");
    }
}