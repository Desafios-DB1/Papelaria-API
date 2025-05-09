using Crosscutting.Dtos.Produto;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Mappers;

public static class ProdutoMapper
{
    public static ProdutoDto MapToDto(this Produto produto)
    {
        return new ProdutoDto
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            CategoriaId = produto.CategoriaId,
            QuantidadeMinima = produto.QuantidadeEstoque.QuantidadeMinima,
            QuantidadeAtual = produto.QuantidadeEstoque.QuantidadeAtual,
            PrecoCompra = produto.PrecoCompra,
            PrecoVenda = produto.PrecoVenda
        };
    }
    public static Produto MapToEntity(this ProdutoDto produtoDto)
    {
        return new Produto
        {
            Nome = produtoDto.Nome,
            Descricao = produtoDto.Descricao,
            CategoriaId = produtoDto.CategoriaId,
            QuantidadeEstoque = new QuantidadeEstoque(produtoDto.QuantidadeAtual, produtoDto.QuantidadeMinima),
            PrecoCompra = produtoDto.PrecoCompra,
            PrecoVenda = produtoDto.PrecoVenda
        };
    }
}