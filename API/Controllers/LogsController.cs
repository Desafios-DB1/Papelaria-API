using Crosscutting.Dtos.Log;
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
    [ProducesResponseType(typeof(ErrorResponse),500)]
    public async Task<IActionResult> ObterLogsPorProdutoId(Guid produtoId)
    {
        var result = await query.ObterPorProdutoIdAsync(produtoId);
        return Ok(result);
    }
}