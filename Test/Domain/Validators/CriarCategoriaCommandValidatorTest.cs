using Domain.Repositories;
using Domain.Validadores;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class CriarCategoriaCommandValidatorTest
{
    private readonly Mock<ICategoriaRepository> _categoriaRepository = new();
    private readonly CriarCategoriaCommandValidator _validator;

    public CriarCategoriaCommandValidatorTest()
    {
        _validator = new CriarCategoriaCommandValidator(_categoriaRepository.Object);
    }
    
    [Fact]
    public async Task Validate_QuandoCategoriaValida_DeveRetornarSucesso()
    {
        _categoriaRepository.Setup(x => x.ExisteComNome(It.IsAny<string>()))
            .Returns(false);
        
        var command = CategoriaBuilder.Novo()
            .ComNome("Teste")
            .ComDescricao("Teste")
            .CriarCategoriaCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task Validate_QuandoNomeVazio_DeveRetornarErro()
    {
        var command = CategoriaBuilder.Novo().ComNome(string.Empty).CriarCategoriaCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var command = CategoriaBuilder.Novo().ComNome(new string('a', 201)).CriarCategoriaCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }

    [Fact]
    public async Task Validate_QuandoExisteProdutoComMesmoNome_DeveRetornarErro()
    {
        _categoriaRepository.Setup(x => x.ExisteComNome(It.IsAny<string>()))
            .Returns(true);
        
        var command = CategoriaBuilder.Novo().ComNome("Teste").CriarCategoriaCommand();
        
        var resultado = await _validator.ValidateAsync(command);
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Já existe um(a) Produto com esse Nome.");
    }
    
    [Fact]
    public async Task Validate_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var command = CategoriaBuilder.Novo().ComDescricao(new string('a', 301)).CriarCategoriaCommand();

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
}