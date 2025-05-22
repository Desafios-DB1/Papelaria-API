using Domain.Interfaces;
using Domain.Repositories;

namespace Domain.Services;

public class CategoriaService(IProdutoRepository produtoRepository) : ICategoriaService
{
    public async Task<bool> PodeRemoverCategoria(Guid categoriaId)
        => !await produtoRepository.ExisteComCategoriaId(categoriaId);
}