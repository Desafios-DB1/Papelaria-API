using Crosscutting.Dtos.LogProduto;

namespace Domain.Interfaces;

public interface ILogQuery
{
    Task<IEnumerable<LogDto>> ObterPorProdutoId(Guid produtoId);
    Task<IEnumerable<LogDto>> ObterPorUsuarioId(string usuarioId);
}