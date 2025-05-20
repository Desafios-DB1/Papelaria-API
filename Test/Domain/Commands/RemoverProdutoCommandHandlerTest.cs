using Crosscutting.Exceptions;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Commands;

public class RemoverProdutoCommandHandlerTest
{
    private readonly Mock<IProdutoRepository> _repository = new();
    private readonly RemoverProdutoCommandHandler _commandHandler;
    
    public RemoverProdutoCommandHandlerTest()
    {
        _commandHandler = new RemoverProdutoCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task Handler_QuandoRequisicaoValida_DeveRemoverProduto()
    {
        var produto = ProdutoBuilder.Novo().Build();
        var command = new RemoverProdutoCommand { Id = produto.Id };
        
        _repository.Setup(r => r.ObterPorIdAsync(produto.Id))
            .ReturnsAsync(produto);
        _repository.Setup(r => r.RemoverESalvarAsync(It.IsAny<Produto>()))
            .Returns(Task.CompletedTask);
        
        await _commandHandler.Handle(command, CancellationToken.None);
        
        _repository.Verify(r => r.RemoverESalvarAsync(produto), Times.Once);
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
        var produtoId = Guid.NewGuid();
        var command = new RemoverProdutoCommand { Id = produtoId };
        
        _repository.Setup(r => r.ObterPorIdAsync(produtoId))
            .ReturnsAsync((Produto)null);
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<NaoEncontradoException>()
            .WithMessage("Produto não existe.");
    }

    [Fact]
    public async Task Handler_QuandoErroNoBanco_DeveLancarException()
    {
        var produtoId = Guid.NewGuid();
        var produto = ProdutoBuilder.Novo().ComId(produtoId).Build();
        var command = new RemoverProdutoCommand { Id = produtoId };
        
        _repository.Setup(r => r.ObterPorIdAsync(produtoId))
            .ReturnsAsync(produto);
        _repository.Setup(r => r.RemoverESalvarAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Erro ao salvar no banco."));
        
        Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao salvar no banco.");

    }
}