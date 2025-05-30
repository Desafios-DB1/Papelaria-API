using System.Security.Claims;
using API.Exemplos.Produto;
using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;
using Crosscutting.Erros;
using Domain.Commands.Produto;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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
    /// <response code="422">Requisição não atende as regras de validação</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 422)]
    [SwaggerRequestExample(typeof(CriarProdutoCommand), typeof(CriarProdutoCommandExample))]
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
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
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
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Produto não existe</response>
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet("nome/{nome}")]
    [ProducesResponseType(typeof(ProdutoDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse),404)]
    [ProducesResponseType(typeof(ErrorResponse),500)]
    public async Task<IActionResult> ObterProdutoPorNome([FromRoute] string nome)
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
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet("categoria/{nomeCategoria}")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
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
    /// <response code="500">Erro interno</response>
    [Authorize]
    [HttpGet("status")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> ObterProdutosPorStatusEstoque(StatusEstoque statusEstoque)
    {
        var result = await query.ObterPorStatusEstoque(statusEstoque);
        return Ok(result);
    }
    
    /// <summary>
    /// Atualizar um produto
    /// </summary>
    /// <response code="200">Produto atualizado com sucesso</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="422">Requisição não atende as regras de validação</response>
    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 422)]
    [SwaggerRequestExample(typeof(AtualizarProdutoCommand), typeof(AtualizarProdutoCommandExample))]
    public async Task<IActionResult> AtualizarProduto([FromRoute] Guid id, AtualizarProdutoCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var result = await mediator.Send(request, cancellationToken);
        
        if (result == Guid.Empty)
            return BadRequest();

        return Ok(result);
    }

    /// <summary>
    /// Remover um produto pelo id
    /// </summary>
    /// <response code="204">Produto excluido com sucesso</response>
    /// <response code="400">Erro ao excluir produto</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Produto não encontrado</response>
    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType( 204)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<IActionResult> RemoverProdutoPorId([FromRoute] Guid id, CancellationToken cancellationToken)
    { 
        var request = new RemoverProdutoCommand { Id = id};
        await mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Atualiza o estoque de um produto
    /// </summary>
    /// <response code="200">Estoque atualizado com sucesso</response>
    /// <response code="401">Sem autorização</response>
    /// <response code="404">Produto não encontrado</response>
    [Authorize]
    [HttpPatch("estoque")]
    [ProducesResponseType(typeof(ProdutoDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse),404)]
    public async Task<IActionResult> AlterarEstoque([FromBody] AlterarEstoqueCommand request, CancellationToken cancellationToken)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized("Usuário não autenticado.");
        
        request.PreencherUsuarioId(usuarioId);
        
        var result = await mediator.Send(request, cancellationToken);
        
        return result is null ? NotFound() : Ok(result);
    }
}