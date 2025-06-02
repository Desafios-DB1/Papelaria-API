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
            .OrderByDescending(l => l.DataCriacao)
            .Select(l => l.MapToDto())
            .ToListAsync();

    public async Task<IEnumerable<LogDto>> ObterPorUsuarioIdAsync(string usuarioId)
        => await context.LogsProduto
            .AsNoTracking()
            .Include(l => l.Produto)
            .Include(l => l.Usuario)
            .Where(l => l.UsuarioId == usuarioId)
            .OrderByDescending(l => l.DataCriacao)
            .Select(l => l.MapToDto())
            .ToListAsync();

    public async Task<IEnumerable<LogDto>> ObterTodosAsync()
        => await context.LogsProduto
            .AsNoTracking()
            .Include(l => l.Produto)
            .Include(l => l.Usuario)
            .OrderByDescending(l => l.DataCriacao)
            .Select(l => l.MapToDto())
            .ToListAsync();
}