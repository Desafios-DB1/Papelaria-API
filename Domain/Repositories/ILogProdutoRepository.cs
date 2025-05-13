using Domain.Entities;

namespace Domain.Repositories;

public interface ILogProdutoRepository : IRepository<LogProduto>
{
    Task<List<LogProduto>> ObterPorProdutoIdAsync(Guid produtoId);
    Task<List<LogProduto>> ObterPorUsuarioIdAsync(string usuarioId);
}