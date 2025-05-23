using API.Controllers;
using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;
using Domain.Interfaces;
using Domain.Mappers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Domain.Builders;

namespace Test.API;

public class CategoriaControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<ICategoriaQuery> _query = new();
    private readonly CategoriaController _controller;

    public CategoriaControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new CategoriaController(_mediator.Object, _query.Object);
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

    #region ObterCategoriaPorId

    [Fact]
    public async Task ObterCategoriaPorId_QuandoCategoriaEncontrada_DeveRetornarOk()
    {
        var categoriaId = Guid.NewGuid();
        var categoria = CategoriaBuilder.Novo()
            .ComId(categoriaId)
            .Build().MapToDto();

        _query
            .Setup(m => m.ObterPorId(categoriaId))
            .ReturnsAsync(categoria);
        
        var result = await _controller.ObterCategoriaPorId(categoriaId);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(categoria);
    }
    
    [Fact]
    public async Task ObterCategoriaPorId_QuandoCategoriaNaoEncontrada_DeveRetornarNotFound()
    { 
        _query
            .Setup(m => m.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync((CategoriaDto)null);
        
        var result = await _controller.ObterCategoriaPorId(Guid.NewGuid());

        result.Should().BeOfType<NotFoundResult>();
    }

    #endregion
    
    #region AtualizarCategoria
    
    [Fact]
    public async Task AtualizarCategoria_QuandoCategoriaAtualizar_DeveRetornarIdDaCategoria()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        var command = CategoriaBuilder.Novo()
            .ComId(categoria.Id)
            .AtualizarCategoriaCommand();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoria.Id);
        
        var result = await _controller.AtualizarCategoria(categoria.Id, command, CancellationToken.None);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(categoria.Id);
    }

    [Fact]
    public async Task AtualizarProduto_QuandoProdutoNaoAtualizado_DeveRetornarBadRequest()
    {
        var command = new AtualizarCategoriaCommand();

        _mediator
            .Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.Empty);
        
        var result = await _controller.AtualizarCategoria(Guid.NewGuid(), command, CancellationToken.None);

        result.Should().BeOfType<BadRequestResult>();
    }
    
    #endregion

    #region RemoverCategoria

    [Fact]
    public async Task RemoverCategoria_QuandoCategoriaRemovida_DeveRetornarNoContent()
    {
        var categoriaId = Guid.NewGuid();
        
        var result = await _controller.RemoverCategoriaPorId(categoriaId, CancellationToken.None);
        
        _mediator.Verify(m => m.Send(It.IsAny<RemoverCategoriaCommand>(), It.IsAny<CancellationToken>()), Times.Once);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task RemoverCategoria_QuandoFalha_DeveLancarException()
    {
        var categoriaId = Guid.NewGuid();
        
        _mediator
            .Setup(m => m.Send(It.IsAny<RemoverCategoriaCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro ao remover categoria"));
        
        Func<Task> act = async () => await _controller.RemoverCategoriaPorId(categoriaId, CancellationToken.None);
        
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao remover categoria");
    }

    #endregion
}