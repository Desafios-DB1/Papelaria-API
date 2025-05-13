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
}