using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CategoriaRepository(ApplicationDbContext context) : Repository<Categoria>(context), ICategoriaRepository
{
    public async Task<Categoria> ObterPorNomeAsync(string nome)
    {
        return await Context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nome.ToLower().Equals(nome.ToLower()));
    }

    public bool ExisteComNome(string nome)
    {
        return Context.Categorias.Any(c => c.Nome.ToLower().Equals(nome.ToLower()));
    }
}