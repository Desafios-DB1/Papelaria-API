using Bogus;
using Domain.ValueObjects;

namespace Test.Domain.Builders;

public class QuantidadeEstoqueBuilder
{
    private Faker<QuantidadeEstoque> _faker;

    public static QuantidadeEstoqueBuilder Novo()
    {
        return new QuantidadeEstoqueBuilder
        {
            _faker = new Faker<QuantidadeEstoque>()
                .RuleFor(p => p.QuantidadeAtual, f => f.Random.Number(1, 100))
                .RuleFor(p => p.QuantidadeMinima, f => f.Random.Number(1, 100))
        };
    }

    public QuantidadeEstoqueBuilder ComQuantidadeMinima(int quantidadeMinima)
    {
        _faker.RuleFor(p=>p.QuantidadeMinima, quantidadeMinima);
        return this;
    }
    public QuantidadeEstoqueBuilder ComQuantidadeAtual(int quantidadeAtual)
    {
        _faker.RuleFor(p=>p.QuantidadeAtual, quantidadeAtual);
        return this;
    }

    public QuantidadeEstoque Build()
    {
        return _faker.Generate();
    }
}