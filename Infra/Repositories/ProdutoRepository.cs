using Crosscutting.Enums;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ProdutoRepository(ApplicationDbContext context) : Repository<Produto>(context), IProdutoRepository
{
    public async Task<Produto> ObterPorNomeAsync(string nome)
    {
        return await Context.Produtos
            .AsNoTracking()
            .Where(p => p.Nome == nome)
            .FirstOrDefaultAsync();
    }

    public bool ExisteComNome(string nome)
    {
        return Context.Produtos.Any(p => p.Nome.ToLower().Equals(nome.ToLower()));
    }

    public async Task<List<Produto>> ObterPorCategoriaAsync(Guid categoriaId)
    {
        return await Context.Produtos
            .AsNoTracking()
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
    }

    public async Task<List<Produto>> ObterPorStatusEstoque(StatusEstoque statusEstoque)
    {
        return await Context.Produtos
            .AsNoTracking()
            .Where(p => p.QuantidadeEstoque.StatusEstoque == statusEstoque)
            .ToListAsync();
    }
}