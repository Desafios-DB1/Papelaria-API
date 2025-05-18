using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    public Task<Categoria> ObterPorNomeAsync(string nome);
    public bool ExisteComNome(string nome);
}