using Domain.Commands.Categoria;
using Swashbuckle.AspNetCore.Filters;

namespace API.Exemplos.Categoria;

public class AtualizarCategoriaCommandExample : IExamplesProvider<AtualizarCategoriaCommand>
{
    public AtualizarCategoriaCommand GetExamples()
    {
        return new AtualizarCategoriaCommand
        {
            Nome = "Categoria Atualizada",
            Descricao = "Descrição da categoria atualizada",
            Ativo = true
        };
    }
}