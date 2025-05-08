using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<Guid> CriarAsync(CategoriaCreationRequestDto categoriaDto);
    Task<CategoriaResponseDto> ObterPorIdAsync(Guid id);
}