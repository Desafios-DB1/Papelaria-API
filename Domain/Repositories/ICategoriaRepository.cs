using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    public Task<List<Categoria>> ObterPorNomeAsync(string nome);
}