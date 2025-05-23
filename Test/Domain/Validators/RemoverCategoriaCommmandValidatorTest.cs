using Domain.Commands.Categoria;
using Domain.Repositories;
using Domain.Validadores;
using FluentAssertions;
using Moq;

namespace Test.Domain.Validators;

public class RemoverCategoriaCommmandValidatorTest
{
    private readonly Mock<IProdutoRepository> _produtoRepository = new();
    private readonly RemoverCategoriaCommandValidator _validator;

    public RemoverCategoriaCommmandValidatorTest()
    {
        _validator = new RemoverCategoriaCommandValidator(_produtoRepository.Object);
    }
    
    [Fact]
    public async Task Validate_QuandoCategoriaValida_DeveRetornarSucesso()
    {
        _produtoRepository
            .Setup(r => r.ExisteComCategoriaId(It.IsAny<Guid>()))
            .Returns(false);
        
        var command = new RemoverCategoriaCommand { CategoriaId = Guid.NewGuid() };

        var validationResult = await _validator.ValidateAsync(command);

        validationResult.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task Validate_QuandoCategoriaIdVazio_DeveRetornarErro()
    {
        var command = new RemoverCategoriaCommand();

        var validationResult = await _validator.ValidateAsync(command);

        validationResult.Errors.Should().Contain(e =>
            e.PropertyName == "CategoriaId" &&
            e.ErrorMessage == "Categoria Id é obrigatório.");
    }

    [Fact]
    public async Task Validate_QuandoCategoriaTemProdutos_DeveRetornarErro()
    {
        var command = new RemoverCategoriaCommand { CategoriaId = Guid.NewGuid() };
        _produtoRepository.Setup(r => r.ExisteComCategoriaId(It.IsAny<Guid>()))
            .Returns(true);
        
        var validationResult = await _validator.ValidateAsync(command);
        
        validationResult.Errors.Should().Contain(e =>
            e.PropertyName == "CategoriaId" &&
            e.ErrorMessage == "A categoria possui produtos associados e não pode ser removida.");
    }
}