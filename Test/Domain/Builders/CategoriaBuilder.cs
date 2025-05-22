using Bogus;
using Domain.Commands.Categoria;
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
    
    public CategoriaBuilder ComId(Guid id)
    {
        _faker.RuleFor(c => c.Id, id);
        return this;
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

    public CriarCategoriaCommand CriarCategoriaCommand()
    {
        var categoria = _faker.Generate();

        return new CriarCategoriaCommand
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo
        };
    }

    public AtualizarCategoriaCommand AtualizarCategoriaCommand()
    {
        var categoria = _faker.Generate();

        return new AtualizarCategoriaCommand
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo
        };
    }
}