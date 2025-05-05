using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class LogProdutoRepository(ApplicationDbContext context) : Repository<LogProduto>(context), ILogProdutoRepository
{
    public async Task<List<LogProduto>> ObterPorProdutoIdAsync(Guid produtoId)
    {
        return await Context.LogsProduto
            .AsNoTracking()
            .Where(l => l.ProdutoId == produtoId)
            .ToListAsync();
    }

    public async Task<List<LogProduto>> ObterPorUsuarioIdAsync(Guid usuarioId)
    {
        return await Context.LogsProduto
            .AsNoTracking()
            .Where(l => l.UsuarioId == usuarioId)
            .ToListAsync();
    }
}