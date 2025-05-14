using Domain.Repositories;
using Test.Domain.Builders;
using Domain.Validadores;
using FluentAssertions;
using Moq;

namespace Test.Domain.Validators;

public class CriarProdutoCommandValidatorTest
{
    private readonly Mock<IProdutoRepository> _produtoRepository = new();
    private readonly CriarProdutoCommandValidator _validator;

    public CriarProdutoCommandValidatorTest()
    {
        _validator = new CriarProdutoCommandValidator(_produtoRepository.Object);
    }
    
    [Fact]
    public async Task Validate_QuandoProdutoValido_DeveRetornarSucesso()
    {
        _produtoRepository.Setup(x => x.ExisteComNome(It.IsAny<string>()))
            .Returns(false);
        
        var command = ProdutoBuilder.Novo()
            .ComNome("Teste")
            .ComDescricao("Teste")
            .CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task Validate_QuandoNomeVazio_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComNome(string.Empty).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComNome(new string('a', 201)).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }

    [Fact]
    public async Task Validate_QuandoExisteProdutoComMesmoNome_DeveRetornarErro()
    {
        _produtoRepository.Setup(x => x.ExisteComNome(It.IsAny<string>()))
            .Returns(true);
        
        var command = ProdutoBuilder.Novo().ComNome("Teste").CriarProdutoCommand();
        
        var resultado = await _validator.ValidateAsync(command);
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Já existe um(a) Produto com esse Nome.");
    }
    
    [Fact]
    public async Task Validate_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComDescricao(new string('a', 301)).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
    
    [Fact]
    public async Task Validate_QuandoQuantidadeMinimaMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComQuantidadeMinima(-1).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeMinima" &&
            e.ErrorMessage == "Quantidade Minima deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoQuantidadeAtualMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComQuantidadeAtual(-1).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeAtual" &&
            e.ErrorMessage == "Quantidade Atual deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoPrecoCompraMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComPrecoCompra(-1).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoCompra" &&
            e.ErrorMessage == "Preco Compra deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoPrecoVendaMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComPrecoVenda(-1).CriarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoVenda" &&
            e.ErrorMessage == "Preco Venda deve ser no mínimo 0.");
    }
}