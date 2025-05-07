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
    
    #region ObterPorId

    [Fact]
    public async Task ObterPorIdAsync_QuandoIdValido_DeveRetornarCategoria()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        _categoriaRepositoryMock.Setup(r =>
            r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(categoria);
        
        var result = await _categoriaService.ObterPorIdAsync(categoria.Id);
        result.Should().NotBeNull();
        result.Should().BeOfType<CategoriaResponseDto>();
        result.Should().BeEquivalentTo(categoria.MapToResponseDto());
    }
    
    [Fact]
    public async Task ObterPorIdAsync_QuandoIdNaoExiste_DeveRetornarNaoEncontradoException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        
        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Categoria)null);
        
        Func<Task> act = async () => await _categoriaService.ObterPorIdAsync(categoria.Id);
        
        await act.Should()
            .ThrowAsync<NaoEncontradoException>()
            .WithMessage("Categoria não existe.");
    }
    
    [Fact]
    public async Task ObterPorIdAsync_QuandoIdVazio_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _categoriaService.ObterPorIdAsync(Guid.Empty);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O ID de categoria não pode ser nulo.");
    }
    
    [Fact]
    public async Task ObterPorIdAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        
        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _categoriaService.ObterPorIdAsync(categoria.Id);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }
    #endregion

    #region ObterPorNome

    [Fact]
    public async Task ObterPorNome_QuandoNomeValido_DeveRetornarListaDeCategorias()
    {
        var categorias = new List<Categoria>
        {
            CategoriaBuilder.Novo().ComNome("teste").Build(),
            CategoriaBuilder.Novo().ComNome("teste").Build(),
            CategoriaBuilder.Novo().ComNome("teste2").Build()
        };
        
        _categoriaRepositoryMock.Setup(r => r.ObterPorNomeAsync(It.IsAny<string>()))
            .ReturnsAsync(categorias.Where(c => c.Nome == "teste").ToList());
        
        var result = await _categoriaService.ObterPorNome("teste");
        result.Should().NotBeNull();
        result.Should().BeOfType<List<CategoriaResponseDto>>();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(categorias.Where(c => c.Nome == "teste").Select(c => c.MapToResponseDto()));
    }
    
    [Fact]
    public async Task ObterPorNome_QuandoNomeInvalido_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _categoriaService.ObterPorNome(string.Empty);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O campo nome não pode ser nulo.");
    }

    
    [Fact]
    public async Task ObterPorNome_QuandoErroNoBanco_DeveLancarException()
    {
        _categoriaRepositoryMock.Setup(r => r.ObterPorNomeAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _categoriaService.ObterPorNome("teste");

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }
    #endregion
}