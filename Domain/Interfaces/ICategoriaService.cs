using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;
using Domain.Querys;

namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<Guid> CriarAsync(CriarCategoriaCommand request, CancellationToken cancellationToken);
    Task<CategoriaResponseDto> ObterPorIdAsync(Guid id);
    Task<CategoriaResponseDto> ObterPorNomeAsync(ObterCategoriaPorNomeQuery request, CancellationToken cancellationToken);
    Task<Guid> AtualizarAsync(CategoriaUpdateRequestDto categoriaDto);
    Task RemoverAsync(Guid id);
    Task<List<CategoriaResponseDto>> ObterTodosAsync();
}