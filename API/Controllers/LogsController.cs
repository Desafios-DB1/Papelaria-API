﻿using Crosscutting.Dtos.Log;
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
    /// <response code="401">Sem autorização</response>
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet("produto/{produtoId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<LogDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> ObterLogsPorProdutoId(Guid produtoId)
    {
        var result = await query.ObterPorProdutoIdAsync(produtoId);
        return Ok(result);
    }
    
    /// <summary>
    /// Obtem os logs de alterações em produtos feitos por um usuário pelo id do usuário
    /// </summary>
    /// <response code="200">Lista de logs (pode ser vazia)</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet("usuario/{usuarioId}")]
    [ProducesResponseType(typeof(IEnumerable<LogDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> ObterLogsPorUsuarioId(string usuarioId)
    {
        var result = await query.ObterPorUsuarioIdAsync(usuarioId);
        return Ok(result);
    }

    /// <summary>
    /// Obtem todos os logs
    /// </summary>
    /// <response code="200">Lista de logs (pode ser vazia)</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LogDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> ObterTodosLogs()
    {
        var result = await query.ObterTodosAsync();
        return Ok(result);
    }
}