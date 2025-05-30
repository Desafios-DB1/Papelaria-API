using Domain.Commands.Categoria;
using Domain.Commands.Produto;
using Swashbuckle.AspNetCore.Filters;

namespace API.Exemplos.Produto;

public class AtualizarProdutoCommandExample : IExamplesProvider<AtualizarProdutoCommand>
{
    public AtualizarProdutoCommand GetExamples()
    {
        return new AtualizarProdutoCommand
        {
            Nome = "Produto Atualizado",
            Descricao = "Descrição do produto atualizado",
            QuantidadeMinima = 5,
            QuantidadeAtual = 10,
            PrecoCompra = 5,
            PrecoVenda = 6,
            CategoriaId = new Guid("4b19be9c-96cb-4153-bcf7-09f3510e5349"),
            Ativo = true
        };
    }
}