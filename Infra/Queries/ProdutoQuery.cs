using Crosscutting.Dtos.Produto;
using Domain.Interfaces;
using Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infra.Queries;

public class ProdutoQuery (ApplicationDbContext context)
    : IProdutoQuery
{
    public async Task<List<ProdutoDto>> ObterTodos()
        => await context.Produtos
            .AsQueryable()
            .Include(p => p.Categoria)
            .Select(p => p.MapToDto())
            .ToListAsync();
    
    public async Task<ProdutoDto> ObterPorNome(string nome)
        => await context.Produtos
            .AsQueryable()
            .Include(p => p.Categoria)
            .Where(p => p.Nome == nome)
            .Select(p => p.MapToDto())
            .FirstOrDefaultAsync();
}