using Crosscutting.Dtos.Log;

namespace Domain.Interfaces;

public interface ILogQuery
{
    Task<IEnumerable<LogDto>> ObterPorProdutoIdAsync(Guid produtoId);
    Task<IEnumerable<LogDto>> ObterPorUsuarioIdAsync(string usuarioId);
}