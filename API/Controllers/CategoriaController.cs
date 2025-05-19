using Crosscutting.Erros;
using Domain.Commands.Categoria;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller de categorias
/// </summary>
[Route("api/categoria")]
[ApiController]
public class CategoriaController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Cria uma categoria e salva no banco
    /// </summary>
    /// <response code="201">Categoria criada com sucesso</response>
    /// <response code="400">Erro ao criar categoria</response>
    /// <response code="401">Sem autorização</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 401)]
    public async Task<IActionResult> CriarCategoria(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result == Guid.Empty)
            return BadRequest();

        return CreatedAtAction(nameof(CriarCategoria), new { id = result }, result);
    }
}