using Crosscutting.Exceptions;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Commands;

public class AtualizarProdutoCommandHandlerTest
{
    private readonly Mock<IProdutoRepository> _repository = new();
    private readonly AtualizarProdutoCommandHandler _commandHandler;

    public AtualizarProdutoCommandHandlerTest()
    {
        _commandHandler = new AtualizarProdutoCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task Handler_QuandoRequisicaoValida_DeveAtualizarProdutoERetornarId()
    {
       var idEsperado = Guid.NewGuid();
       var produto = ProdutoBuilder.Novo().ComId(idEsperado).Build();
       var command = ProdutoBuilder.Novo()
           .ComId(idEsperado)
           .AtualizarProdutoCommand();

       _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(produto);
       _repository.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Produto>()))
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
    public async Task Handler_QuandoNaoExisteProduto_DeveLancarNaoEncontradoException()
    {
        var command = ProdutoBuilder.Novo().AtualizarProdutoCommand();
        
        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Produto)null);
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NaoEncontradoException>()
            .WithMessage("Produto não existe.");
    }

    [Fact]
    public async Task Handler_QuandoErroNoBanco_DeveLancarException()
    {
        var produto = ProdutoBuilder.Novo().Build();
        var command = ProdutoBuilder.Novo().AtualizarProdutoCommand();
        
        _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(produto);
        _repository.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Erro ao salvar no banco de dados."));
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao salvar no banco de dados.");
    }
}