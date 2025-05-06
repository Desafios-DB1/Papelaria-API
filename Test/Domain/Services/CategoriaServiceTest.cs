using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Mappers;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Services;

public class CategoriaServiceTest
{
    private readonly CategoriaService _categoriaService;
    private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock = new();
    private readonly Mock<IValidator<CategoriaCreationRequestDto>> _categoriaValidatorMock = new();

    public CategoriaServiceTest()
    {
        _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object, _categoriaValidatorMock.Object);
    }
    
    #region CriarCategoria
    
    [Fact]
    public async Task CriarAsync_QuandoDtoValido_DeveRetornarId()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        
        _categoriaRepositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Categoria>()))
            .ReturnsAsync(categoria.Id);

        var result = await _categoriaService.CriarAsync(categoria.MapToCreationDto());
        result.Should().Be(categoria.Id);
    }

    [Fact]
    public async Task CriarAsync_QuandoDtoNulo_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _categoriaService.CriarAsync(null);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("Campo inválido: categoria");
    }
    
    [Fact]
    public async Task CriarAsync_QuandoDtoInvalido_DeveLancarRequisicaoInvalidaException()
    {
        var categoria = CategoriaBuilder.Novo().Build(); 
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Nome", "Campo inválido: Nome")
        });
        
        _categoriaValidatorMock
            .Setup(v => 
                v.ValidateAsync(It.IsAny<CategoriaCreationRequestDto>(), CancellationToken.None))
            .ReturnsAsync(validationResult);

        Func<Task> act = async () => await _categoriaService.CriarAsync(categoria.MapToCreationDto());
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("A requisição possui erros de validação");
    }
    
    [Fact]
    public async Task CriarAsync_QuandoErroNoBanco_DeveLancarErroNoBancoException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        
        _categoriaRepositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Categoria>()))
            .ReturnsAsync(Guid.Empty);
        _categoriaValidatorMock.Setup(v =>
            v.ValidateAsync(It.IsAny<CategoriaCreationRequestDto>(), CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        Func<Task> act = async () => await _categoriaService.CriarAsync(categoria.MapToCreationDto());
        
        await act.Should()
            .ThrowAsync<ErroNoBancoException>()
            .WithMessage("Ocorreu um erro no banco de dados.");
    }
    #endregion
}