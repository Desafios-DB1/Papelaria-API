namespace Domain.Interfaces;

public interface ICategoriaService
{
    Task<bool> PodeRemoverCategoria(Guid categoriaId);
}