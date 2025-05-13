using Crosscutting.Dtos.Produto;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Mappers;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Services;

public class ProdutoServiceTest
{
    private readonly ProdutoService _service;
    private readonly Mock<IProdutoRepository> _repositoryMock = new();

    public ProdutoServiceTest()
    {
        _service = new ProdutoService(_repositoryMock.Object);
    }

    #region CriarProduto

    [Fact]
    public async Task CriarAsync_QuandoDtoValido_DeveRetornarId()
    {
        var produto = ProdutoBuilder.Novo().Build();

        _repositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Produto>()))
            .ReturnsAsync(produto.Id);

        var result = await _service.CriarAsync(produto.MapToDto());
        result.Should().Be(produto.Id);
    }
    
    [Fact]
    public async Task CriarAsync_QuandoDtoNulo_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _service.CriarAsync(null);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O objeto produto não pode ser nulo.");
    }
    
    [Fact]
    public async Task CriarAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var produto = ProdutoBuilder.Novo().Build();
        
        _repositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _service.CriarAsync(produto.MapToDto());

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");

    }

    #endregion

    #region ObterPorId

    [Fact]
    public async Task ObterPorIdAsync_QuandoIdValido_DeveRetornarProduto()
    {
        var produto = ProdutoBuilder.Novo().Build();

        _repositoryMock.Setup(r =>
            r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(produto);
        
        var result = await _service.ObterPorIdAsync(produto.Id);
        result.Should().NotBeNull();
        result.Should().BeOfType<ProdutoDto>();
        result.Should().BeEquivalentTo(produto.MapToDto());
    }
    
    [Fact]
    public async Task ObterPorIdAsync_QuandoIdNaoExiste_DeveRetornarNaoEncontradoException()
    {
        var produto = ProdutoBuilder.Novo().Build();
        
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Produto)null);
        
        Func<Task> act = async () => await _service.ObterPorIdAsync(produto.Id);
        
        await act.Should()
            .ThrowAsync<NaoEncontradoException>()
            .WithMessage("Produto não existe.");
    }
    
    [Fact]
    public async Task ObterPorIdAsync_QuandoIdVazio_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _service.ObterPorIdAsync(Guid.Empty);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O campo id do objeto produto não pode ser nulo.");
    }
    
    [Fact]
    public async Task ObterPorIdAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var produto = ProdutoBuilder.Novo().Build();
        
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _service.ObterPorIdAsync(produto.Id);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }

    #endregion
    
    #region ObterTodos
    
    [Fact]
    public async Task ObterTodosAsync_QuandoExistemProdutos_DeveRetornarListaDeProdutos()
    {
        var produtos = new List<Produto>
        {
            ProdutoBuilder.Novo().Build(),
            ProdutoBuilder.Novo().Build()
        };
    
        _repositoryMock.Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(produtos);
    
        var result = await _service.ObterTodosAsync();
    
        result.Should().NotBeNull();
        result.Should().HaveCount(produtos.Count);
        result.Should().AllBeOfType<ProdutoDto>();
        result.Should().BeEquivalentTo(produtos.Select(c => c.MapToDto()));
    }
    
    [Fact]
    public async Task ObterTodosAsync_QuandoNaoExistemProdutos_DeveRetornarListaVazia()
    {
        _repositoryMock.Setup(r => r.ObterTodosAsync())
            .ReturnsAsync([]);
    
        var result = await _service.ObterTodosAsync();
    
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task ObterTodosAsync_QuandoErroNoBanco_DeveLancarException()
    {
        _repositoryMock.Setup(r => r.ObterTodosAsync())
            .ThrowsAsync(new Exception("Falha no banco."));
    
        Func<Task> act = async () => await _service.ObterTodosAsync();
    
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }
    
    #endregion

    #region AtualizarProduto

    [Fact]
    public async Task AtualizarAsync_QuandoDtoValido_DeveRetornarId()
    {
        var produto = ProdutoBuilder.Novo().Build();

        _repositoryMock.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Produto>()))
            .ReturnsAsync(produto.Id);
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(produto);

        var result = await _service.AtualizarAsync(produto.Id, produto.MapToDto());
        result.Should().Be(produto.Id);
    }

    [Fact]
    public async Task AtualizarAsync_QuandoIdNaoExiste_DeveRetornarNaoEncontradoException()
    {
        var produto = ProdutoBuilder.Novo().Build();
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Produto)null);

        Func<Task> act = async () => await _service.AtualizarAsync(produto.Id, produto.MapToDto());

        await act.Should()
            .ThrowAsync<NaoEncontradoException>()
            .WithMessage("Produto não existe.");
    }

    [Fact]
    public async Task AtualizarAsync_QuandoDtoInvalido_DeveRetornarRequisicaoInvalidaException()
    {
        var produto = ProdutoBuilder.Novo().Build();

        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(produto);

        Func<Task> act = async () => await _service.AtualizarAsync(Guid.Empty, null);

        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O objeto produto não pode ser nulo.");
    }

    [Fact]
    public async Task AtualizarAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var produto = ProdutoBuilder.Novo().Build();

        _repositoryMock.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(produto);

        Func<Task> act = async () => await _service.AtualizarAsync(produto.Id, produto.MapToDto());

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }
    
    #endregion
    
    
}