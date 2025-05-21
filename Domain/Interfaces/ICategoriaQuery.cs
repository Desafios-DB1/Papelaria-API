using Crosscutting.Dtos.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaQuery
{
    Task<CategoriaDto> ObterPorId(Guid id);
}