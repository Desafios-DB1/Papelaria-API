using API.Controllers;
using Domain.Commands.Categoria;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.API;

public class CategoriaControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly CategoriaController _controller;

    public CategoriaControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new CategoriaController(_mediator.Object);
    }

    #region CriarCategoria

    [Fact]
    public async Task CriarCategoria_QuandoCategoriaCriadaComSucesso_DeveRetornarCreatedAtAction()
    {
        var command = new CriarCategoriaCommand();
        var categoriaId = Guid.NewGuid();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoriaId);
        
        var result = await _controller.CriarCategoria(command, CancellationToken.None);
        
        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.Value.Should().Be(categoriaId);
    }

    [Fact]
    public async Task CriarCategoria_QuandoCategoriaNaoForCriada_DeveRetornarBadRequest()
    {
        var command = new CriarCategoriaCommand();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.Empty);
        
        var result = await _controller.CriarCategoria(command, CancellationToken.None);

        result.Should().BeOfType<BadRequestResult>();
    }

    #endregion
}