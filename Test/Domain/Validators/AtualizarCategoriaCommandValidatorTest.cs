using Domain.Repositories;
using Domain.Validadores;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class AtualizarCategoriaCommandValidatorTest
{
    private readonly Mock<ICategoriaRepository> _categoriaRepository = new();
    private readonly AtualizarCategoriaCommandValidator _validator;

    public AtualizarCategoriaCommandValidatorTest()
    {
        _validator = new AtualizarCategoriaCommandValidator(_categoriaRepository.Object);
    }
    
    [Fact]
    public async Task Validate_QuandoCategoriaValida_DeveRetornarSucesso()
    {
        _categoriaRepository
            .Setup(r => r.ExisteComNome(It.IsAny<string>()))
            .Returns(false);
        
        var command = CategoriaBuilder.Novo()
            .ComNome("Teste")
            .ComDescricao("Teste")
            .AtualizarCategoriaCommand();

        var validationResult = await _validator.ValidateAsync(command);

        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task Validate_QuandoNomeVazio_DeveRetornarErro()
    {
        var command = CategoriaBuilder.Novo()
            .ComNome(string.Empty)
            .AtualizarCategoriaCommand();

        var validationResult = await _validator.ValidateAsync(command);

        validationResult.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var command = CategoriaBuilder.Novo()
            .ComNome(new string('a', 201))
            .AtualizarCategoriaCommand();

        var validationResult = await _validator.ValidateAsync(command);

        validationResult.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }

    [Fact]
    public async Task Validate_QuandoExisteCategoriaComMesmoNome_DeveRetornarErro()
    {
        _categoriaRepository.Setup(x => x.ExisteComNome(It.IsAny<string>()))
            .Returns(true);
        
        var command = CategoriaBuilder.Novo()
            .ComNome("Teste")
            .AtualizarCategoriaCommand();
        
        var validationResult = await _validator.ValidateAsync(command);
        
        validationResult.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Já existe um(a) Categoria com esse Nome.");
    }
    
    [Fact]
    public async Task Validate_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var command = CategoriaBuilder.Novo()
            .ComDescricao(new string('a', 301))
            .AtualizarCategoriaCommand();

        var validationResult = await _validator.ValidateAsync(command);

        validationResult.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
}