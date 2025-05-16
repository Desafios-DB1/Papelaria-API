using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;

namespace Domain.Interfaces;

public interface IProdutoQuery
{
    Task<List<ProdutoDto>> ObterTodos();
    Task<ProdutoDto> ObterPorNome(string nome);
    Task<List<ProdutoDto>> ObterPorNomeCategoria(string nomeCategoria);
    Task<List<ProdutoDto>> ObterPorStatusEstoque(StatusEstoque statusEstoque);
}