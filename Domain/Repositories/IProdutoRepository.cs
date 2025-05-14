using Crosscutting.Enums;
using Domain.Entities;

namespace Domain.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task <Produto> ObterPorNomeAsync(string nome);
    bool ExisteComNome(string nome);
    Task <List<Produto>> ObterPorCategoriaAsync(Guid categoriaId);
    Task <List<Produto>> ObterPorStatusEstoque(StatusEstoque statusEstoque);
}