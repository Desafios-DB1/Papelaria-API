using Crosscutting.Validators.Produto;
using Test.Domain.Builders;
using Domain.Mappers;
using FluentAssertions;

namespace Test.Domain.Validators;

public class ProdutoDtoValidatorTest
{
    private readonly ProdutoDtoValidator _validator = new();

    [Fact]
    public void ProdutoDtoValidator_QuandoProdutoValido_DeveRetornarSucesso()
    {
        var produto = ProdutoBuilder.Novo().Build();

        var resultado = _validator.Validate(produto.MapToDto());

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoNomeVazio_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComNome(string.Empty).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComNome(new string('a', 201)).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComDescricao(new string('a', 301)).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoQuantidadeMinimaMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComQuantidadeMinima(-1).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeMinima" &&
            e.ErrorMessage == "Quantidade Minima deve ser no mínimo 0.");
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoQuantidadeAtualMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComQuantidadeAtual(-1).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeAtual" &&
            e.ErrorMessage == "Quantidade Atual deve ser no mínimo 0.");
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoPrecoCompraMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComPrecoCompra(-1).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoCompra" &&
            e.ErrorMessage == "Preco Compra deve ser no mínimo 0.");
    }
    
    [Fact]
    public void ProdutoDtoValidator_QuandoPrecoVendaMenorQueZero_DeveRetornarErro()
    {
        var produto = ProdutoBuilder.Novo().ComPrecoVenda(-1).Build();

        var resultado = _validator.Validate(ProdutoMapper.MapToDto(produto));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoVenda" &&
            e.ErrorMessage == "Preco Venda deve ser no mínimo 0.");
    }
}