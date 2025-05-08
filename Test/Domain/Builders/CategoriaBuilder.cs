using Bogus;
using Domain.Entities;

namespace Test.Domain.Builders;

public class CategoriaBuilder
{
    private Faker<Categoria> _faker;

    public static CategoriaBuilder Novo()
    {
        return new CategoriaBuilder
        {
            _faker = new Faker<Categoria>()
                .RuleFor(c => c.Nome, f => f.Name.FirstName())
                .RuleFor(c => c.Descricao, f => f.Lorem.Paragraph())
                .RuleFor(c => c.Ativo, f => f.Random.Bool())
        };
    }

    public CategoriaBuilder ComNome(string nome)
    {
        _faker.RuleFor(c => c.Nome, nome);
        return this;
    }
    
    public CategoriaBuilder ComDescricao(string descricao)
    {
        _faker.RuleFor(c => c.Descricao, descricao);
        return this;
    }

    public Categoria Build()
    {
        return _faker.Generate();
    }
}