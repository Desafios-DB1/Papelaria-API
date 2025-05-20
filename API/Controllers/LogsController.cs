using Crosscutting.Dtos.LogProduto;
using Crosscutting.Erros;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller de logs
/// </summary>
[Route("api/logs")]
[ApiController]
public class LogsController(ILogQuery query) : ControllerBase
{
    /// <summary>
    /// Obtem os logs de um produto pelo id do produto
    /// </summary>
    /// <response code="200">Lista de logs (pode ser vazia)</response>
    /// <response code="400">Erro ao obter logs</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet("{produtoId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<LogDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task <IActionResult> ObterLogsPorProdutoId(Guid produtoId)
    {
        var result = await query.ObterPorProdutoId(produtoId);
        return Ok(result);
    }
    
    /// <summary>
    /// Obtem os logs de um produto pelo id do usuario
    /// </summary>
    /// <response code="200">Lista de logs (pode ser vazia)</response>
    /// <response code="400">Erro ao obter logs</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet("usuario/{usuarioId}")]
    [ProducesResponseType(typeof(IEnumerable<LogDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> ObterLogsPorUsuarioId(string usuarioId)
    {
        var result = await query.ObterPorUsuarioId(usuarioId);
        return Ok(result);
    }

    /// <summary>
    /// Obtem todos os logs de todos os produtos
    /// </summary>
    /// <response code="200">Lista de logs (pode ser vazia)</response>
    /// <response code="400">Erro ao obter logs</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LogDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> ObterTodos()
    {
        var result = await query.ObterTodos();
        return Ok(result);
    }
}