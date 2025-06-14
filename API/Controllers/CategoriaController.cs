﻿using API.Exemplos.Categoria;
using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Erros;
using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers;

/// <summary>
/// Controller de categorias
/// </summary>
[Route("api/categoria")]
[ApiController]
public class CategoriaController(IMediator mediator, ICategoriaQuery query) : ControllerBase
{
    /// <summary>
    /// Cria uma categoria
    /// </summary>
    /// <response code="201">Categoria criada com sucesso</response>
    /// <response code="400">Erro ao criar categoria</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="422">Requisição não atende as regras de validação</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 422)]
    [SwaggerRequestExample(typeof(CriarCategoriaCommand), typeof(CriarCategoriaCommandExample))]
    public async Task<IActionResult> CriarCategoria(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result == Guid.Empty)
            return BadRequest();

        return CreatedAtAction(nameof(CriarCategoria), new { id = result }, result);
    }
    
    /// <summary>
    /// Obtém uma categoria pelo id
    /// </summary>
    /// <response code="200">Categoria encontrada</response>
    /// <response code="404">Categoria não encontrada</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoriaDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> ObterCategoriaPorId([FromRoute] Guid id)
    {
        var result = await query.ObterPorId(id);

        if (result == null)
            throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));

        return Ok(result);
    }

    /// <summary>
    /// Obtém todas as categorias
    /// </summary>
    /// <response code="200">Lista de categorias (pode ser vazia)</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoriaDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> ObterTodos()
    {
        var result = await query.ObterTodos();
        return Ok(result);
    }
    
    /// <summary>
    /// Atualizar uma categoria
    /// </summary>
    /// <response code="200">Categoria atualizada com sucesso</response>
    /// <response code="400">Erro ao atualizar categoria</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Categoria não encontrada</response>
    /// <response code="422">Requisição não atende as regras de validação</response>
    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 422)]
    [SwaggerRequestExample(typeof(AtualizarCategoriaCommand), typeof(AtualizarCategoriaCommandExample))]
    public async Task<IActionResult> AtualizarCategoria([FromRoute] Guid id, AtualizarCategoriaCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var result = await mediator.Send(request, cancellationToken);
        
        if (result == Guid.Empty)
            return BadRequest();

        return Ok(result);
    }

    /// <summary>
    /// Remover uma categoria
    /// </summary>
    /// <response code="204">Categoria removida com sucesso</response>
    /// <response code="400">Erro ao remover a categoria</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Categoria não encontrada</response>
    /// <response code="422">Requisição não atende as regras de validação</response>
    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 422)]
    public async Task<IActionResult> RemoverCategoriaPorId([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new RemoverCategoriaCommand { CategoriaId = id };
        await mediator.Send(request, cancellationToken);
        return NoContent();
    }
}