using API.Controllers;
using Crosscutting.Dtos.Produto;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.API;

public class ProdutoControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly ProdutoController _controller;
    
    public ProdutoControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new ProdutoController(_mediator.Object);
    }
    
    [Fact]
    public async Task CriarProduto_QuandoProdutoCriadoComSucesso_DeveRetornarCreatedAtAction()
    {
        var produtoDto = new ProdutoDto();
        var produtoId = Guid.NewGuid();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(produtoId);
        
        var result = await _controller.CriarProduto(produtoDto, CancellationToken.None);
        
        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.Value.Should().Be(produtoId);
    }

    [Fact]
    public async Task CriarProduto_DeveRetornarBadRequest_QuandoProdutoNaoForCriado()
    {
        var produtoDto = new ProdutoDto();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.Empty);
        
        var result = await _controller.CriarProduto(produtoDto, CancellationToken.None);

        result.Should().BeOfType<BadRequestResult>();
    }
}