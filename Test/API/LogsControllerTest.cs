using API.Controllers;
using Crosscutting.Dtos.Log;
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
    public async Task ObterLogsPorProdutoId_QuandoHouverLogs_DeveRetornarOkComListaDeLogs()
    {
        var produtoId = Guid.NewGuid();
        var logs = new List<LogDto>
        {
            new()
            {
                NomeUsuarioResponsavel = "Usuario1",
                NomeProduto = "Produto1",
                TipoOperacao = TipoOperacao.Entrada,
                QuantidadeAnterior = 10,
                QuantidadeAtual = 20,
                DataHora = DateTime.UtcNow
            }
        };
        _query.Setup(m => m.ObterPorProdutoIdAsync(produtoId)).ReturnsAsync(logs);
        
        var result = await _controller.ObterLogsPorProdutoId(produtoId);
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }
    
    [Fact]
    public async Task ObterLogsPorProdutoId_QuandoNaoHouverLogs_DeveRetornarOkComListaVazia()
    {
        var produtoId = Guid.NewGuid();
        var logs = new List<LogDto>();
        _query.Setup(m => m.ObterPorProdutoIdAsync(produtoId)).ReturnsAsync(logs);
        
        var result = await _controller.ObterLogsPorProdutoId(produtoId);
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(logs);
    }
    
    [Fact]
    public async Task ObterLogsPorProdutoId_QuandoErroNoBanco_DeveLancarException()
    {
        var produtoId = Guid.NewGuid();
        _query.Setup(m => m.ObterPorProdutoIdAsync(produtoId)).ThrowsAsync(new Exception("Erro no banco"));
        
        Func<Task> act = async () => await _controller.ObterLogsPorProdutoId(produtoId);
        await act.Should().ThrowAsync<Exception>().WithMessage("Erro no banco");
    }

    #endregion
}