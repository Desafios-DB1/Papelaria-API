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
    /// <response code="401">Sem autorização</response>
    [Authorize]
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
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ObterProdutos()
    {
        var result = await query.ObterTodos();
        return Ok(result);
    }

    /// <summary>
    /// Obter produto por nome
    /// </summary>
    /// <param name="nome">Nome do produto a ser procurado</param>
    /// <response code="200">Objeto produto</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> ObterProdutosPorNome([FromRoute] string nome)
    {
        var result = await query.ObterPorNome(nome);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Obter todos os produtos de uma categoria
    /// </summary>
    /// <param name="nomeCategoria">Nome da categoria</param>
    /// <response code="200">Lista de produtos dessa categoria</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet("categoria/{nomeCategoria}")]
    public async Task<IActionResult> ObterProdutosPorCategoria([FromRoute] string nomeCategoria)
    {
        var result = await query.ObterPorCategoria(nomeCategoria);
        return Ok(result);
    }
}