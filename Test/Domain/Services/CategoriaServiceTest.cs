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
    public CategoriaServiceTest()
    {
        _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object);
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
            .WithMessage("O DTO de categoria não pode ser nulo.");
    }
    
    [Fact]
    public async Task CriarAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        
        _categoriaRepositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Categoria>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _categoriaService.CriarAsync(categoria.MapToCreationDto());

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");

    }
    #endregion
    
    #region RemoverCategoria

    [Fact]
    public async Task RemoverAsync_QuandoIdValido_DeveRemoverCategoria()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        _categoriaRepositoryMock.Setup(r => r.RemoverESalvarAsync(It.IsAny<Categoria>()));
        
        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(categoria.Id))
            .ReturnsAsync(categoria);
        
        await _categoriaService.RemoverAsync(categoria.Id);
        
        _categoriaRepositoryMock.Verify(r => r.RemoverESalvarAsync(It.IsAny<Categoria>()), Times.Once);
    }
    #endregion
}