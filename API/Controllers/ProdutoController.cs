using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;
using Crosscutting.Erros;
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
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse),401)]
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
    /// <response code="200">Lista de produtos (pode ser vazia)</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse),401)]
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
    [ProducesResponseType(typeof(ProdutoDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse),404)]
    [ProducesResponseType(typeof(ErrorResponse),401)]
    public async Task<IActionResult> ObterProdutosPorNome([FromRoute] string nome)
    {
        var result = await query.ObterPorNome(nome);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Obter todos os produtos de uma categoria
    /// </summary>
    /// <param name="nomeCategoria">Nome da categoria</param>
    /// <response code="200">Lista de produtos dessa categoria (pode estar vazia)</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpGet("categoria/{nomeCategoria}")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse),401)]
    public async Task<IActionResult> ObterProdutosPorNomeCategoria([FromRoute] string nomeCategoria)
    {
        var result = await query.ObterPorNomeCategoria(nomeCategoria);
        return Ok(result);
    }

    /// <summary>
    /// Obter todos os produtos com um determinado status de estoque
    /// </summary>
    /// <param name="statusEstoque">Status do estoque que ira filtrar os produtos</param>
    /// <response code="200">Lista de produtos com esse status (pode estar vazia)</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Status enviado é inválido</response>
    [Authorize]
    [HttpGet("status")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 401)]
    public async Task<IActionResult> ObterProdutosPorStatusEstoque(StatusEstoque statusEstoque)
    {
        var result = await query.ObterPorStatusEstoque(statusEstoque);
        return Ok(result);
    }

    /// <summary>
    /// Remover um produto pelo id
    /// </summary>
    /// <response code="204">Produto excluido com sucesso</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpDelete("remover")]
    [ProducesResponseType( 204)]
    public async Task<IActionResult> RemoverProdutoPorId(RemoverProdutoCommand request, CancellationToken cancellationToken)
    {
       await mediator.Send(request, cancellationToken);
       return NoContent();
    }
}