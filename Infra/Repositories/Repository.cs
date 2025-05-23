﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : Entidade
{
    protected readonly ApplicationDbContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task SalvarAsync()
    {
        await Context.SaveChangesAsync();
    }

    public async Task<Guid> AdicionarAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task<Guid> AdicionarESalvarAsync(T entity)
    {
        var entityId = await AdicionarAsync(entity);
        await SalvarAsync();
        return entityId;
    }

    public async Task<T> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public Guid Atualizar(T entity)
    {
        var updatedEntity = _dbSet.Update(entity);
        return updatedEntity.Entity.Id;
    }

    public async Task<Guid> AtualizarESalvarAsync(T entity)
    {
        var entityId = Atualizar(entity);
        await SalvarAsync();
        return entityId;
    }

    public void Remover(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task RemoverESalvarAsync(T entity)
    {
        Remover(entity);
        await SalvarAsync();
    }
}