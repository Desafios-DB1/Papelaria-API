namespace Domain.Interfaces;

public interface ICrudService<TDto>
{
    Task<Guid> CriarAsync(TDto dto);
    Task<TDto> ObterPorIdAsync(Guid id);
    Task<List<TDto>> ObterTodosAsync();
    Task<Guid> AtualizarAsync(Guid id, TDto dto);
    Task RemoverAsync(Guid id);
}