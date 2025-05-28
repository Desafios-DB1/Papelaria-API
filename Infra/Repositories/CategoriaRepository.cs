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
            .FirstOrDefaultAsync(c => EF.Functions.Like(c.Nome, nome));
    }

    public bool ExisteComNome(string nome)
    {
        return Context.Categorias.Any(c => c.Nome == nome);
    }

    public bool ExisteComId(Guid id)
    {
        return Context.Categorias.Any(c => c.Id == id);
    }
}