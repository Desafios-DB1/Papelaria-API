using Domain.Commands;
using Domain.Commands.Produto;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller de produtos
/// </summary>
[Route("api/produto")]
[ApiController]
public class ProdutoController(IMediator mediator, IProdutoQuery query) : ControllerBase
{
    /// <summary>
    /// Criar um produto e salva no banco
    /// </summary>
    /// <response code="201">Produto criado com sucesso</response>
    /// <response code="400">Erro ao criar produto</response>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CriarProduto(CriarProdutoCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        
        if (result == Guid.Empty)
            return BadRequest();
        
        return CreatedAtAction(nameof(CriarProduto), new { id = result }, result);
    }

    /// <summary>
    /// Obter todos os produtos cadastrados
    /// </summary>
    /// <response code="200">Lista de produtos</response>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ObterProdutos(CancellationToken cancellationToken)
    {
        var result = await query.ObterTodos();
        return Ok(result);
    }
}