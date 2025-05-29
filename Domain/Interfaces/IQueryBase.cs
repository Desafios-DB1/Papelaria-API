using Domain.Entities;

namespace Domain.Interfaces;

public interface IQueryBase
{
    Task<bool> ExisteEntidadePorIdAsync<TEntity>(Guid id) 
        where TEntity : Entidade;
}