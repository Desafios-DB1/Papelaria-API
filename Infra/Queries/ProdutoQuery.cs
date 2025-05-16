using Crosscutting.Constantes;
using Crosscutting.Dtos.Produto;
using Crosscutting.Enums;
using Crosscutting.Exceptions;
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
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Select(p => p.MapToDto())
            .ToListAsync();
    
    public async Task<ProdutoDto> ObterPorNome(string nome)
        => await context.Produtos
            .AsQueryable()
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Where(p => p.Nome == nome)
            .Select(p => p.MapToDto())
            .FirstOrDefaultAsync();

    public async Task<List<ProdutoDto>> ObterPorNomeCategoria(string nomeCategoria)
        => await context.Produtos
            .AsQueryable()
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Where(p => p.Categoria.Nome == nomeCategoria)
            .Select(p => p.MapToDto())
            .ToListAsync();

    public async Task<List<ProdutoDto>> ObterPorStatusEstoque(StatusEstoque statusEstoque)
    {
        if(!Enum.IsDefined(statusEstoque))
            throw new ArgumentException(ErrorMessages.ObjetoInvalido("Status de Estoque"));
        
        var produtos = await context.Produtos
        .AsQueryable()
        .AsNoTracking()
        .Include(p => p.Categoria)
        .Select(p => p.MapToDto())
        .ToListAsync();

        return produtos
            .Where(p => p.StatusEstoque == statusEstoque)
            .ToList();
    }
}