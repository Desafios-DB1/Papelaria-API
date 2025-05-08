using Crosscutting.Dtos.Produto;
using Domain.Entities;

namespace Domain.Mappers;

public static class ProdutoMapper
{
    public static ProdutoCreationRequestDto MapToCreationDto(this Produto produto)
    {
        return new ProdutoCreationRequestDto
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
}