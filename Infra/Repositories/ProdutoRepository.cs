using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ProdutoRepository(ApplicationDbContext context) : Repository<Produto>(context), IProdutoRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Produto>> ObterPorNomeAsync(string nome)
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .Where(p => p.Nome == nome)
            .ToListAsync();
        
        return produtos;
    }

    public async Task<List<Produto>> ObterPorCategoriaAsync(Guid categoriaId)
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
        
        return produtos;
    }

    public async Task<List<Produto>> ObterPorStatusEstoque(StatusEstoque status)
    {
        var query = _context.Produtos.AsNoTracking();

        if (status == StatusEstoque.OK)
            query = query.Where(p => !p.QuantidadeEstoque.EstoqueCritico);
        else if (status == StatusEstoque.CRITICO)
            query = query.Where(p => p.QuantidadeEstoque.EstoqueCritico);

        return await query.ToListAsync();
    }
}