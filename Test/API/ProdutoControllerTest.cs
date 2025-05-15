using API.Controllers;
using Crosscutting.Dtos.Produto;
using Domain.Commands;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mappers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Domain.Builders;

namespace Test.API;

public class ProdutoControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IProdutoQuery> _query = new();
    private readonly ProdutoController _controller;
    
    public ProdutoControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new ProdutoController(_mediator.Object, _query.Object);
    }

    #region CriarProduto

    [Fact]
    public async Task CriarProduto_QuandoProdutoCriadoComSucesso_DeveRetornarCreatedAtAction()
    {
        var command = new CriarProdutoCommand();
        var produtoId = Guid.NewGuid();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(produtoId);
        
        var result = await _controller.CriarProduto(command, CancellationToken.None);
        
        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.Value.Should().Be(produtoId);
    }

    [Fact]
    public async Task CriarProduto_QuandoProdutoNaoForCriado_DeveRetornarBadRequest()
    {
        var command = new CriarProdutoCommand();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.Empty);
        
        var result = await _controller.CriarProduto(command, CancellationToken.None);

        result.Should().BeOfType<BadRequestResult>();
    }

    #endregion

    #region ObterProdutos

    [Fact]
    public async Task ObterProdutos_QuandoHouverProdutos_DeveRetornarListaDeProdutos()
    {
        var produtos = new List<ProdutoDto>
        {
            ProdutoBuilder.Novo().Build().MapToDto(),
            ProdutoBuilder.Novo().Build().MapToDto()
        };
        
        _query
            .Setup(m => m.ObterTodos())
            .ReturnsAsync(produtos);
        
        var result = await _controller.ObterProdutos();
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(produtos);
    }

    [Fact]
    public async Task ObterProdutos_QuandoNaoHouverProdutos_DeveRetornarListaVazia()
    {
        _query
            .Setup(m => m.ObterTodos())
            .ReturnsAsync(new List<ProdutoDto>());
        
        var result = await _controller.ObterProdutos();
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.As<IEnumerable<object>>().Should().BeEmpty();
    }

    #endregion
    
    #region ObterProdutosPorNome

    [Fact]
    public async Task ObterProdutosPorNome_QuandoHouverProduto_DeveRetornarProduto()
    {
        var produto = ProdutoBuilder.Novo().Build().MapToDto();

        _query
            .Setup(m => m.ObterPorNome(It.IsAny<string>()))
            .ReturnsAsync(produto);
        
        var result = await _controller.ObterProdutosPorNome(produto.Nome);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(produto);
    }
    
    [Fact]
    public async Task ObterProdutosPorNome_QuandoNaoHouverProdutos_DeveRetornarNotFound()
    {
        _query
            .Setup(m => m.ObterPorNome(It.IsAny<string>()))
            .ReturnsAsync(null as ProdutoDto);
        
        var result = await _controller.ObterProdutosPorNome("Teste");
        
        result.Should().BeOfType<NotFoundResult>();
    }

    #endregion
    
    #region ObterProdutosPorCategoria

    [Fact]
    public async Task ObterProdutosPorCategoria_QuandoHouverProdutos_DeveRetornarListaDeProdutos()
    {
        var produtos = new List<ProdutoDto>
        {
            ProdutoBuilder.Novo().Build().MapToDto(),
            ProdutoBuilder.Novo().Build().MapToDto()
        };

        _query
            .Setup(m => m.ObterPorCategoria(It.IsAny<string>()))
            .ReturnsAsync(produtos);
        
        var result = await _controller.ObterProdutosPorCategoria("Teste");
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(produtos);
    }

    #endregion
}