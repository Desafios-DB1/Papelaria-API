using Crosscutting.Dtos.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<Guid> CriarAsync(CategoriaCreationRequestDto categoriaDto);
    Task<CategoriaResponseDto> ObterPorIdAsync(Guid id);
}