using Crosscutting.Dtos.Categoria;

namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<Guid> CriarAsync(CategoriaCreationRequestDto categoriaDto);
    Task<CategoriaResponseDto> ObterPorIdAsync(Guid id);
    Task<CategoriaResponseDto> ObterPorNomeAsync(string nome);
    Task<Guid> AtualizarAsync(CategoriaUpdateRequestDto categoriaDto);
    Task RemoverAsync(Guid id);
    Task<List<CategoriaResponseDto>> ObterTodosAsync();
}