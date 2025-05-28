using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Queries;

public class QueryBase(ApplicationDbContext context) : IQueryBase
{
    public async Task<bool> ExisteEntidadePorIdAsync<TEntity>(Guid id) where TEntity : Entidade
    {
        var dbSet = context.Set<TEntity>();
        return await dbSet.AnyAsync(e => e.Id.Equals(id));
    }
}