using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<Categoria> ObterPorNomeAsync(string nome);
    bool ExisteComNome(string nome);
    bool ExisteComId(Guid id);
}