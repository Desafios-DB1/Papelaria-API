using Domain.Commands.Categoria;
using Swashbuckle.AspNetCore.Filters;

namespace API.Exemplos.Categoria;

public class CriarCategoriaCommandExample : IExamplesProvider<CriarCategoriaCommand>
{
    public CriarCategoriaCommand GetExamples()
    {
        return new CriarCategoriaCommand{
            Nome = "Categoria Exemplo",
            Descricao = "Descrição da categoria exemplo",
            Ativo = true
        };
    }
}