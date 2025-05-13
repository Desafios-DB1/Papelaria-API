using Crosscutting.Dtos.Produto;
using Domain.Commands;
using Domain.Commands.Produto;
using Domain.Entities;

namespace Domain.Factory;

public static class CriarProdutoCommandFactory
{
    public static CriarProdutoCommand CriarProdutoCommand(this Produto produto)
     => new ()
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