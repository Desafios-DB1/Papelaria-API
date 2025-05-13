using Crosscutting.Dtos.Produto;
using Domain.Commands;
using Domain.Commands.Produto;
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
public class ProdutoController(IMediator mediator) : ControllerBase
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
}