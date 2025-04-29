using Domain.Entities;
using Domain.Enums;

namespace Domain.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task <List<Produto>> ObterPorNomeAsync(string nome);
    Task <List<Produto>> ObterPorCategoriaAsync(Guid categoriaId);
    Task <List<Produto>> ObterPorStatusEstoque(bool estoqueEstaCritico);
}