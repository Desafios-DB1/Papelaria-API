using Crosscutting.Dtos.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<CategoriaDto> ObterPorNomeAsync(string nome);
}