using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CategoriaRepository(ApplicationDbContext context) : Repository<Categoria>(context), ICategoriaRepository
{
    public async Task<List<Categoria>> ObterPorNomeAsync(string nome)
    {
        return await Context.Categorias
            .AsNoTracking()
            .Where(c => c.Nome.Equals(nome, StringComparison.CurrentCultureIgnoreCase))
            .ToListAsync();
    }
}