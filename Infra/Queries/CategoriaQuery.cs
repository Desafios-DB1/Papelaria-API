using Crosscutting.Dtos.Categoria;
using Domain.Interfaces;
using Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infra.Queries;

public class CategoriaQuery (ApplicationDbContext context)
    : ICategoriaQuery
{
    public async Task<CategoriaDto> ObterPorId(Guid id)
        => await context.Categorias
            .AsQueryable()
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => c.MapToDto())
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<CategoriaDto>> ObterTodos()
        => await context.Categorias.AsQueryable()
            .Select(c => c.MapToDto())
            .ToListAsync();
}