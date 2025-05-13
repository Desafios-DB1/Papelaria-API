using Crosscutting.Dtos.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaService : ICrudService<CategoriaDto>
{
    Task<CategoriaDto> ObterPorNomeAsync(string nome);
}