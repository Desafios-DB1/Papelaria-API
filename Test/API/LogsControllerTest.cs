using API.Controllers;
using Crosscutting.Dtos.LogProduto;
using Crosscutting.Enums;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Domain.Builders;

namespace Test.API;

public class LogsControllerTest
{
    private readonly Mock<ILogQuery> _query = new();
    private readonly LogsController _controller;

    public LogsControllerTest()
    {
        _controller = new LogsController(_query.Object);
        UsuarioBuilder.SimularUsuarioAutenticado(_controller);
    }

    #region ObterLogsPorProdutoId

    [Fact]
    public async Task ObterLogsPorProdutoId_QuandoLogNaoExistir_DeveRetornarOkComListaVazia()
    {
        var produtoId = Guid.NewGuid();
        var logs = new List<LogDto>();
        
        _query
            .Setup(m => m.ObterPorProdutoId(produtoId))
            .ReturnsAsync(logs);
        
        var result = await _controller.ObterLogsPorProdutoId(produtoId);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }
    
    [Fact]
    public async Task ObterLogsPorProdutoId_QuandoLogExistir_DeveRetornarOkComListaDeLogs()
    {
        var produtoId = Guid.NewGuid();
        var logs = new List<LogDto>
        {
            new()
            {
                UsuarioId = "usuario123",
                ProdutoId = produtoId,
                TipoOperacao = TipoOperacao.Entrada,
                QuantidadeAnterior = 10,
                QuantidadeAtual = 20,
                DataOperacao = DateTime.UtcNow,
            }
        };
        
        _query
            .Setup(m => m.ObterPorProdutoId(produtoId))
            .ReturnsAsync(logs);
        
        var result = await _controller.ObterLogsPorProdutoId(produtoId);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }
    
    #endregion

    #region ObterLogsPorUsuarioId

    [Fact]
    public async Task ObterLogsPoUsuarioId_QuandoLogNaoExistir_DeveRetornarOkComListaVazia()
    {
        const string usuarioId = "usuario123";
        var logs = new List<LogDto>();
        
        _query
            .Setup(m => m.ObterPorUsuarioId(usuarioId))
            .ReturnsAsync(logs);
        
        var result = await _controller.ObterLogsPorUsuarioId(usuarioId);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }
    
    [Fact]
    public async Task ObterLogsPorUsuarioId_QuandoLogExistir_DeveRetornarOkComListaDeLogs()
    {
        const string usuarioId = "usuario123";
        var logs = new List<LogDto>
        {
            new()
            {
                UsuarioId = usuarioId,
                ProdutoId = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Entrada,
                QuantidadeAnterior = 10,
                QuantidadeAtual = 20,
                DataOperacao = DateTime.UtcNow,
            }
        };
        
        _query
            .Setup(m => m.ObterPorUsuarioId(usuarioId))
            .ReturnsAsync(logs);
        
        var result = await _controller.ObterLogsPorUsuarioId(usuarioId);
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }

    #endregion
    
    #region ObterTodos

    [Fact]
    public async Task ObterTodos_QuandoNaoExistirLogs_DeveRetornarOkComListaVazia()
    {
        var logs = new List<LogDto>();
        
        _query
            .Setup(m => m.ObterTodos())
            .ReturnsAsync(logs);
        
        var result = await _controller.ObterTodos();
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }
    
    [Fact]
    public async Task ObterTodos_QuandoLogExistir_DeveRetornarOkComListaDeLogs()
    {
        var logs = new List<LogDto>
        {
            new()
            {
                UsuarioId = "usuario123",
                ProdutoId = Guid.NewGuid(),
                TipoOperacao = TipoOperacao.Entrada,
                QuantidadeAnterior = 10,
                QuantidadeAtual = 20,
                DataOperacao = DateTime.UtcNow,
            }
        };
        
        _query
            .Setup(m => m.ObterTodos())
            .ReturnsAsync(logs);
        
        var result = await _controller.ObterTodos();
        
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }

    #endregion
}