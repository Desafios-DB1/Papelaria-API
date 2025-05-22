using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Commands;

public class AtualizarCategoriaCommandHandlerTest
{
    private readonly Mock<ICategoriaRepository> _repository = new();
    private readonly AtualizarCategoriaCommandHandler _commandHandler;

    public AtualizarCategoriaCommandHandlerTest()
    {
        _commandHandler = new AtualizarCategoriaCommandHandler(_repository.Object);
    }
    
    [Fact]
    public async Task Handler_QuandoRequisicaoValida_DeveAtualizarCategoriaERetornarId()
    {
       var idEsperado = Guid.NewGuid();
       var categoria = CategoriaBuilder.Novo().ComId(idEsperado).Build();
       var command = CategoriaBuilder.Novo()
           .ComId(idEsperado)
           .AtualizarCategoriaCommand();

       _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(categoria);
       _repository.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Categoria>()))
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
    public async Task Handler_QuandoNaoExisteCategoria_DeveLancarNaoEncontradoException()
    {
        var command = CategoriaBuilder.Novo().AtualizarCategoriaCommand();
        
        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Categoria)null);
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NaoEncontradoException>()
            .WithMessage("Categoria não existe.");
    }

    [Fact]
    public async Task Handler_QuandoErroNoBanco_DeveLancarException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        var command = CategoriaBuilder.Novo().AtualizarCategoriaCommand();
        
        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(categoria);
        _repository.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Categoria>()))
            .ThrowsAsync(new Exception("Erro ao salvar no banco de dados."));
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao salvar no banco de dados.");
    }
}