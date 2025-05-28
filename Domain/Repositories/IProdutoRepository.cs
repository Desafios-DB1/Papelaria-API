using Crosscutting.Enums;
using Domain.Entities;

namespace Domain.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task <Produto> ObterPorNomeAsync(string nome);
    bool ExisteComNome(string nome);
    bool ExisteComCategoriaId(Guid categoriaId);
    bool ExisteComId(Guid id);
    Task <List<Produto>> ObterPorCategoriaAsync(Guid categoriaId);
    Task <List<Produto>> ObterPorStatusEstoque(StatusEstoque statusEstoque);
}