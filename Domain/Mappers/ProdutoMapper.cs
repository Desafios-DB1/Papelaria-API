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
}