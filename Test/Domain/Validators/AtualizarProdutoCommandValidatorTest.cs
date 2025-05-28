using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.Validadores;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class AtualizarProdutoCommandValidatorTest
{
    private readonly Mock<IProdutoRepository> _produtoRepository = new();
    private readonly Mock<IQueryBase> _queryBase = new();
    private readonly AtualizarProdutoCommandValidator _validator;

    public AtualizarProdutoCommandValidatorTest()
    {
        _validator = new AtualizarProdutoCommandValidator(_produtoRepository.Object, _queryBase.Object);
    }
    
    [Fact]
    public async Task Validate_QuandoProdutoValido_DeveRetornarSucesso()
    {
        _produtoRepository.Setup(x => x.ExisteComNome(It.IsAny<string>()))
            .Returns(false);
        _queryBase.Setup(x => x.ExisteEntidadePorIdAsync<Categoria>(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        var command = ProdutoBuilder.Novo()
            .ComNome("Teste")
            .ComDescricao("Teste")
            .AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task Validate_QuandoNomeVazio_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComNome(string.Empty).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComNome(new string('a', 201)).AtualizarProdutoCommand();

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
        
        var command = ProdutoBuilder.Novo().ComNome("Teste").AtualizarProdutoCommand();
        
        var resultado = await _validator.ValidateAsync(command);
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Já existe um(a) Produto com esse Nome.");
    }
    
    [Fact]
    public async Task Validate_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComDescricao(new string('a', 301)).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
    
    [Fact]
    public async Task Validate_QuandoQuantidadeMinimaMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComQuantidadeMinima(-1).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeMinima" &&
            e.ErrorMessage == "Quantidade Minima deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoQuantidadeAtualMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComQuantidadeAtual(-1).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeAtual" &&
            e.ErrorMessage == "Quantidade Atual deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoPrecoCompraMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComPrecoCompra(-1).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoCompra" &&
            e.ErrorMessage == "Preco Compra deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoPrecoVendaMenorQueZero_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComPrecoVenda(-1).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "PrecoVenda" &&
            e.ErrorMessage == "Preco Venda deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoCategoriaIdVazio_DeveRetornarErro()
    {
        var command = ProdutoBuilder.Novo().ComCategoriaId(Guid.Empty).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "CategoriaId" &&
            e.ErrorMessage == "Categoria Id é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoCategoriaNaoExiste_DeveRetornarErro()
    {
        _queryBase.Setup(x => x.ExisteEntidadePorIdAsync<Produto>(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        var command = ProdutoBuilder.Novo().ComCategoriaId(Guid.NewGuid()).AtualizarProdutoCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "CategoriaId" &&
            e.ErrorMessage == "Esse(a) Categoria não existe.");
    }
}