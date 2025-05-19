using Bogus;
using Crosscutting.Enums;
using Domain.Commands.Produto;
using Domain.Entities;

namespace Test.Domain.Builders;

public class ProdutoBuilder
{
    private Faker<Produto> _faker;

    public static ProdutoBuilder Novo()
    {
        return new ProdutoBuilder
        {
            _faker = new Faker<Produto>()
                .RuleFor(p => p.Id, f => f.Random.Guid())
                .RuleFor(p => p.Nome, f => f.Name.FirstName())
                .RuleFor(p => p.Descricao, f => f.Lorem.Paragraph())
                .RuleFor(p => p.PrecoCompra, f => f.Random.Number(1, 100))
                .RuleFor(p => p.PrecoVenda, f => f.Random.Number(1, 100))
                .RuleFor(p => p.Ativo, true)
                .RuleFor(p => p.CategoriaId, f => CategoriaBuilder.Novo().Build().Id)
                .RuleFor(p=>p.QuantidadeEstoque, QuantidadeEstoqueBuilder.Novo().Build)
        };
    }
    
    public ProdutoBuilder ComId(Guid id)
    {
        _faker.RuleFor(p => p.Id, id);
        return this;
    }
    
    public ProdutoBuilder ComNome(string nome)
    {
        _faker.RuleFor(p => p.Nome, nome);
        return this;
    }

    public ProdutoBuilder ComDescricao(string descricao)
    {
        _faker.RuleFor(p=>p.Descricao, descricao);
        return this;
    }

    public ProdutoBuilder ComQuantidadeMinima(int quantidadeMinima)
    {
        _faker.RuleFor(p=> p.QuantidadeEstoque, 
            QuantidadeEstoqueBuilder.Novo().ComQuantidadeMinima(quantidadeMinima).Build);
        return this;
    }
    public ProdutoBuilder ComQuantidadeAtual(int quantidadeAtual)
    {
        _faker.RuleFor(p=> p.QuantidadeEstoque, 
            QuantidadeEstoqueBuilder.Novo().ComQuantidadeAtual(quantidadeAtual).Build);
        return this;
    }

    public ProdutoBuilder ComPrecoCompra(int precoCompra)
    {
        _faker.RuleFor(p => p.PrecoCompra, precoCompra);
        return this;
    }

    public ProdutoBuilder ComPrecoVenda(int precoVenda)
    {
        _faker.RuleFor(p=>p.PrecoVenda, precoVenda);
        return this;
    }

    public Produto Build()
    {
        return _faker.Generate();
    }
    
    public CriarProdutoCommand CriarProdutoCommand()
    {
        var produto = _faker.Generate();
        
        return new CriarProdutoCommand
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            PrecoCompra = produto.PrecoCompra,
            PrecoVenda = produto.PrecoVenda,
            Ativo = produto.Ativo,
            CategoriaId = produto.CategoriaId,
            QuantidadeMinima = produto.QuantidadeEstoque.QuantidadeMinima,
            QuantidadeAtual = produto.QuantidadeEstoque.QuantidadeAtual
        };
    }

    public AlterarEstoqueCommand AdicionarEstoqueCommand()
    {
        var produto = _faker.Generate();
        return new AlterarEstoqueCommand
        {
            ProdutoId = produto.Id,
            TipoOperacao = TipoOperacao.Entrada,
            Quantidade = new Random().Next(1, 100)
        };
    }
}