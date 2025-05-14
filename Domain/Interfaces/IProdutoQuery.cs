using Crosscutting.Dtos.Produto;

namespace Domain.Interfaces;

public interface IProdutoQuery
{
    Task<List<ProdutoDto>> ObterTodos();
    Task<ProdutoDto> ObterPorNome(string nome);
}