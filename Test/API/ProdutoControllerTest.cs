using API.Controllers;
using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Commands.Produto;
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
    public async Task ObterProdutosPorNomeCategoria_QuandoHouverProdutos_DeveRetornarListaDeProdutos()
    {
        var produtos = new List<ProdutoDto>
        {
            ProdutoBuilder.Novo().Build().MapToDto(),
            ProdutoBuilder.Novo().Build().MapToDto()
        };

        _query
            .Setup(m => m.ObterPorNomeCategoria(It.IsAny<string>()))
            .ReturnsAsync(produtos);
        
        var result = await _controller.ObterProdutosPorNomeCategoria("Teste");
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(produtos);
    }
    
    [Fact]
    public async Task ObterProdutosPorNomeCategoria_QuandoNaoHouverProdutos_DeveRetornarListaVazia()
    {
        _query
            .Setup(m => m.ObterPorNomeCategoria(It.IsAny<string>()))
            .ReturnsAsync(new List<ProdutoDto>());
        
        var result = await _controller.ObterProdutosPorNomeCategoria("Teste");
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.As<IEnumerable<object>>().Should().BeEmpty();
    }

    #endregion
    
    #region ObterProdutosPorStatusEstoque

    [Fact]
    public async Task ObterProdutosPorStatusEstoque_QuandoHouverProdutos_DeveRetornarListaDeProdutos()
    {
        var produtos = new List<ProdutoDto>
        {
            ProdutoBuilder.Novo().Build().MapToDto(),
            ProdutoBuilder.Novo().Build().MapToDto()
        };

        _query
            .Setup(m => m.ObterPorStatusEstoque(It.IsAny<StatusEstoque>()))
            .ReturnsAsync(produtos);
        
        var result = await _controller.ObterProdutosPorStatusEstoque(StatusEstoque.CRITICO);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(produtos);
    }
    
    [Fact]
    public async Task ObterProdutosPorStatusEstoque_QuandoNaoHouverProdutos_DeveRetornarListaVazia()
    {
        _query
            .Setup(m => m.ObterPorStatusEstoque(It.IsAny<StatusEstoque>()))
            .ReturnsAsync(new List<ProdutoDto>());
        
        var result = await _controller.ObterProdutosPorStatusEstoque(StatusEstoque.CRITICO);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.As<IEnumerable<object>>().Should().BeEmpty();
    }

    [Fact]
    public async Task ObterProdutosPorStatusEstoque_QuandoStatusInvalido_DeveLancarException()
    {
        _query
            .Setup(m => m.ObterPorStatusEstoque(It.IsAny<StatusEstoque>()))
            .ThrowsAsync(new ArgumentException("O status de estoque fornecido é inválido"));

        Func<Task> act = async () => await _controller.ObterProdutosPorStatusEstoque((StatusEstoque)999);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("O status de estoque fornecido é inválido");
    }

    #endregion
    
    #region AtualizarProduto
    
    [Fact]
    public async Task AtualizarProduto_QuandoProdutoAtualizado_DeveRetornarIdDoProduto()
    {
        var produto = ProdutoBuilder.Novo().Build();
        var command = ProdutoBuilder.Novo()
            .ComId(produto.Id)
            .AtualizarProdutoCommand();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto.Id);
        
        var result = await _controller.AtualizarProduto(command, CancellationToken.None);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(produto.Id);
    }

    [Fact]
    public async Task AtualizarProduto_QuandoProdutoNaoAtualizado_DeveRetornarBadRequest()
    {
        var command = new AtualizarProdutoCommand();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.Empty);
        
        var result = await _controller.AtualizarProduto(command, CancellationToken.None);

        result.Should().BeOfType<BadRequestResult>();
    }
    
    #endregion

    #region RemoverProdutoPorId

    [Fact]
    public async Task RemoverProdutoPorId_QuandoSucesso_DeveRetornarNoContent()
    {
        var idEsperado = Guid.NewGuid();
        
        var result = await _controller.RemoverProdutoPorId(idEsperado, CancellationToken.None);
        
        _mediator.Verify(x => x.Send(It.IsAny<RemoverProdutoCommand>(), 
                    It.IsAny<CancellationToken>()), Times.Once);
        
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task RemoverProdutoPorId_QuandoFalha_DeveLancarException()
    {
        var idEsperado = Guid.Empty;

        _mediator
            .Setup(m => m.Send(It.IsAny<RemoverProdutoCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Falha ao remover produto"));
        
        Func<Task> act = async () => await _controller.RemoverProdutoPorId(idEsperado, CancellationToken.None);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha ao remover produto");
    }
    #endregion
}