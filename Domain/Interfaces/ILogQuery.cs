using Crosscutting.Dtos.Log;

namespace Domain.Interfaces;

public interface ILogQuery
{
    Task<IEnumerable<LogDto>> ObterPorProdutoIdAsync(Guid produtoId);
}