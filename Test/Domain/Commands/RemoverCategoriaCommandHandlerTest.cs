using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Commands;

public class RemoverCategoriaCommandHandlerTest
{
    private readonly Mock<ICategoriaRepository> _repository = new();
    private readonly RemoverCategoriaCommandHandler _commandHandler;
    
    public RemoverCategoriaCommandHandlerTest()
    {
        _commandHandler = new RemoverCategoriaCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task Handler_QuandoRequisicaoValida_DeveRemoverCategoria()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        var command = new RemoverCategoriaCommand { CategoriaId = categoria.Id };

        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(categoria);

        _repository.Setup(r => r.RemoverESalvarAsync(It.IsAny<Categoria>()))
            .Returns(Task.CompletedTask);

        await _commandHandler.Handle(command, CancellationToken.None);
        
        _repository.Verify(r => r.RemoverESalvarAsync(categoria), Times.Once);
    }

    [Fact]
    public async Task Handler_QuandoRequisicaoNula_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = () => _commandHandler.Handle(null, CancellationToken.None);
        
        await act.Should().ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("A requisição é inválida.");
    }
    
    [Fact]
    public async Task Handler_QuandoCategoriaNaoExiste_DeveLancarNaoEncontradoException()
    {
        var command = new RemoverCategoriaCommand { CategoriaId = Guid.NewGuid() };

        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Categoria)null);

        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<NaoEncontradoException>()
            .WithMessage("Categoria não existe.");
    }

    [Fact]
    public async Task Handler_QuandoErroNoBanco_DeveLancarException()
    {
        var command = new RemoverCategoriaCommand { CategoriaId = Guid.NewGuid() };
        
        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Erro ao remover do banco."));
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao remover do banco.");
    }
}