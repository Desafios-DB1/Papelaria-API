using Domain.Factory;
using Test.Domain.Builders;
using Domain.Validadores;
using FluentAssertions;

namespace Test.Domain.Validators;

public class CriarProdutoCommandValidatorTest
{
    private readonly CriarProdutoCommandValidator _validator = new();

    [Fact]
    public void Validate_QuandoProdutoValido_DeveRetornarSucesso()
    {
        var produto = ProdutoBuilder.Novo().Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Validate_QuandoNomeVazio_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComNome(string.Empty).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public void Validate_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComNome(new string('a', 201)).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }
    
    [Fact]
    public void Validate_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComDescricao(new string('a', 301)).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
    
    [Fact]
    public void Validate_QuandoQuantidadeMinimaMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComQuantidadeMinima(-1).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeMinima" &&
            e.ErrorMessage == "Quantidade Minima deve ser no mínimo 0.");
    }
    
    [Fact]
    public void Validate_QuandoQuantidadeAtualMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComQuantidadeAtual(-1).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeAtual" &&
            e.ErrorMessage == "Quantidade Atual deve ser no mínimo 0.");
    }
    
    [Fact]
    public void Validate_QuandoPrecoCompraMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComPrecoCompra(-1).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoCompra" &&
            e.ErrorMessage == "Preco Compra deve ser no mínimo 0.");
    }
    
    [Fact]
    public void Validate_QuandoPrecoVendaMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComPrecoVenda(-1).Build();
        var command = produto.CriarProdutoCommand();

        var resultado = _validator.Validate(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoVenda" &&
            e.ErrorMessage == "Preco Venda deve ser no mínimo 0.");
    }
}