using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<Guid> CriarAsync(CriarCategoriaCommand request, CancellationToken cancellationToken);
    Task<CategoriaResponseDto> ObterPorIdAsync(Guid id);
    Task<CategoriaResponseDto> ObterPorNomeAsync(string nome);
    Task<Guid> AtualizarAsync(CategoriaUpdateRequestDto categoriaDto);
    Task RemoverAsync(Guid id);
    Task<List<CategoriaResponseDto>> ObterTodosAsync();
}