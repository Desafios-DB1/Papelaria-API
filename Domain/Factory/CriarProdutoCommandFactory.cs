using Crosscutting.Dtos.Produto;
using Domain.Commands.Produto;

namespace Domain.Factory;

public static class CriarProdutoCommandFactory
{
    public static CriarProdutoCommand CriarProdutoCommand(this ProdutoDto dto)
     => new ()
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            CategoriaId = dto.CategoriaId,
            QuantidadeMinima = dto.QuantidadeMinima,
            QuantidadeAtual = dto.QuantidadeAtual,
            PrecoCompra = dto.PrecoCompra,
            PrecoVenda = dto.PrecoVenda
        };
    
}