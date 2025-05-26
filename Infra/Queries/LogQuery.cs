using Crosscutting.Dtos.Log;
using Domain.Interfaces;
using Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infra.Queries;

public class LogQuery(ApplicationDbContext context) : ILogQuery
{
    public async Task<IEnumerable<LogDto>> ObterPorProdutoIdAsync(Guid produtoId)
        => await context.LogsProduto
            .AsNoTracking()
            .Include(l => l.Produto)
            .Include(l => l.Usuario)
            .Where(l => l.ProdutoId == produtoId)
            .Select(l => l.MapToDto())
            .ToListAsync();
}