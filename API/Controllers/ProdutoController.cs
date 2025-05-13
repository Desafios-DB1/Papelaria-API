using Crosscutting.Dtos.Produto;
using Domain.Factory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller de produtos
/// </summary>
[Route("api/produto")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProdutoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Criar um produto e salva no banco
    /// </summary>
    /// <response code="201">Produto criado com sucesso</response>
    /// <response code="400">Erro ao criar produto</response>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CriarProduto(ProdutoDto dto, CancellationToken cancellationToken)
    {
        var command = dto.CriarProdutoCommand();

        var result = await _mediator.Send(command, cancellationToken);
        
        if (result == Guid.Empty)
            return BadRequest();
        
        return CreatedAtAction(nameof(CriarProduto), new { id = result }, result);
    }
}