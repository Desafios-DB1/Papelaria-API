using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ProdutoRepository(ApplicationDbContext context) : Repository<Produto>(context), IProdutoRepository
{
    public async Task<List<Produto>> ObterPorNomeAsync(string nome)
    {
        var produtos = await Context.Produtos
            .AsNoTracking()
            .Where(p => p.Nome == nome)
            .ToListAsync();
        
        return produtos;
    }

    public async Task<List<Produto>> ObterPorCategoriaAsync(Guid categoriaId)
    {
        var produtos = await Context.Produtos
            .AsNoTracking()
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
        
        return produtos;
    }

    public async Task<List<Produto>> ObterPorStatusEstoque(bool estoqueEstaCritico)
    {
        var produtos = await Context.Produtos
            .AsNoTracking()
            .Where(p => p.QuantidadeEstoque.EstoqueCritico == estoqueEstaCritico)
            .ToListAsync();

        return produtos;
    }
}