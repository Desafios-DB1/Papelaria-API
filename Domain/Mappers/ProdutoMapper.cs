using Crosscutting.Dtos.Produto;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Mappers;

public static class ProdutoMapper
{
    public static Produto MapToEntity(this CriarProdutoCommand command)
    {
        return new Produto
        {
            Id = Guid.NewGuid(),
            Nome = command.Nome,
            Descricao = command.Descricao,
            Ativo = command.Ativo,
            CategoriaId = command.CategoriaId,
            QuantidadeEstoque = new QuantidadeEstoque(command.QuantidadeAtual, command.QuantidadeMinima),
            PrecoCompra = command.PrecoCompra,
            PrecoVenda = command.PrecoVenda
        };
    }

    public static ProdutoDto MapToDto(this Produto produto)
    {
        return new ProdutoDto
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Ativo = produto.Ativo,
            PrecoCompra = produto.PrecoCompra,
            PrecoVenda = produto.PrecoVenda,
            CategoriaId = produto.CategoriaId,
            CategoriaNome = produto.Categoria?.Nome ?? string.Empty,
            QuantidadeMinima = produto.QuantidadeEstoque.QuantidadeMinima,
            QuantidadeAtual = produto.QuantidadeEstoque.QuantidadeAtual,
            StatusEstoque = produto.QuantidadeEstoque.StatusEstoque
        };
    }
}