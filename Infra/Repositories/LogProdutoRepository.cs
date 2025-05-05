using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class LogProdutoRepository(ApplicationDbContext context) : Repository<LogProduto>(context), ILogProdutoRepository
{
    public async Task<List<LogProduto>> ObterPorProdutoIdAsync(Guid produtoId)
    {
        var logs = await Context.LogsProduto
            .AsNoTracking()
            .Where(l => l.ProdutoId == produtoId)
            .ToListAsync();

        return logs;
    }

    public async Task<List<LogProduto>> ObterPorUsuarioIdAsync(Guid usuarioId)
    {
        var logs = await Context.LogsProduto
            .AsNoTracking()
            .Where(l => l.UsuarioId == usuarioId)
            .ToListAsync();

        return logs;
    }
}