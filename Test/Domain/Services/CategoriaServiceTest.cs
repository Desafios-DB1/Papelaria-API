﻿using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Mappers;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
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

        var result = await _categoriaService.CriarAsync(categoria.MapToDto());
        result.Should().Be(categoria.Id);
    }

    [Fact]
    public async Task CriarAsync_QuandoDtoNulo_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _categoriaService.CriarAsync(null);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O objeto categoria não pode ser nulo.");
    }
    
    [Fact]
    public async Task CriarAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        
        _categoriaRepositoryMock.Setup(r => r.AdicionarESalvarAsync(It.IsAny<Categoria>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _categoriaService.CriarAsync(categoria.MapToDto());

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
        result.Should().BeOfType<CategoriaDto>();
        result.Should().BeEquivalentTo(categoria.MapToDto());
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
            .WithMessage("O campo id do objeto categoria não pode ser nulo.");
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

    #region ObterPorNomeAsync

    [Fact]
    public async Task ObterPorNome_QuandoNomeValido_DeveRetornarListaDeCategorias()
    {
        var categoria = CategoriaBuilder.Novo().ComNome("teste").Build();
        
        _categoriaRepositoryMock.Setup(r => r.ObterPorNomeAsync(It.IsAny<string>()))
            .ReturnsAsync(categoria);
        
        var result = await _categoriaService.ObterPorNomeAsync("teste");
        result.Should().NotBeNull();
        result.Should().BeOfType<CategoriaDto>();
        result.Should().BeEquivalentTo(categoria.MapToDto());
    }
    
    [Fact]
    public async Task ObterPorNome_QuandoNomeInvalido_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _categoriaService.ObterPorNomeAsync(string.Empty);
        
        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O campo nome não pode ser nulo.");
    }

    
    [Fact]
    public async Task ObterPorNome_QuandoErroNoBanco_DeveLancarException()
    {
        _categoriaRepositoryMock.Setup(r => r.ObterPorNomeAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        
        Func<Task> act = async () => await _categoriaService.ObterPorNomeAsync("teste");

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }

    #endregion

    #region AtualizarCategoria

    [Fact]
    public async Task AtualizarAsync_QuandoDtoValido_DeveRetornarId()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        _categoriaRepositoryMock.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Categoria>()))
            .ReturnsAsync(categoria.Id);
        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(categoria);

        var result = await _categoriaService.AtualizarAsync(categoria.Id, categoria.MapToDto());
        result.Should().Be(categoria.Id);
    }

    [Fact]
    public async Task AtualizarAsync_QuandoIdNaoExiste_DeveRetornarNaoEncontradoException()
    {
        var categoria = CategoriaBuilder.Novo().Build();
        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Categoria)null);

        Func<Task> act = async () => await _categoriaService.AtualizarAsync(categoria.Id, categoria.MapToDto());

        await act.Should()
            .ThrowAsync<NaoEncontradoException>()
            .WithMessage("Categoria não existe.");
    }

    [Fact]
    public async Task AtualizarAsync_QuandoDtoInvalido_DeveRetornarRequisicaoInvalidaException()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(categoria);

        Func<Task> act = async () => await _categoriaService.AtualizarAsync(Guid.Empty, null);

        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O objeto categoria não pode ser nulo.");
    }

    [Fact]
    public async Task AtualizarAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        _categoriaRepositoryMock.Setup(r => r.AtualizarESalvarAsync(It.IsAny<Categoria>()))
            .ThrowsAsync(new Exception("Falha no banco."));
        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(categoria);

        Func<Task> act = async () => await _categoriaService.AtualizarAsync(categoria.Id, categoria.MapToDto());

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

        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(categoria.Id))
            .ReturnsAsync(categoria);

        await _categoriaService.RemoverAsync(categoria.Id);

        _categoriaRepositoryMock.Verify(r => r.RemoverESalvarAsync(It.IsAny<Categoria>()), Times.Once);
    }

    [Fact]
    public async Task RemoverAsync_QuandoIdNaoExiste_DeveRetornarNaoEncontradoException()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        Func<Task> act = async () => await _categoriaService.RemoverAsync(categoria.Id);

        await act.Should()
            .ThrowAsync<NaoEncontradoException>()
            .WithMessage("Categoria não existe.");
    }

    [Fact]
    public async Task RemoverAsync_QuandoIdVazio_DeveLancarRequisicaoInvalidaException()
    {
        Func<Task> act = async () => await _categoriaService.RemoverAsync(Guid.Empty);

        await act.Should()
            .ThrowAsync<RequisicaoInvalidaException>()
            .WithMessage("O campo id do objeto Categoria não pode ser nulo.");
    }

    [Fact]
    public async Task RemoverAsync_QuandoErroNoBanco_DeveLancarException()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        _categoriaRepositoryMock.Setup(r => r.ObterPorIdAsync(categoria.Id))
            .ReturnsAsync(categoria);

        _categoriaRepositoryMock.Setup(r => r.RemoverESalvarAsync(It.IsAny<Categoria>()))
            .ThrowsAsync(new Exception("Falha no banco."));

        Func<Task> act = async () => await _categoriaService.RemoverAsync(categoria.Id);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }
    #endregion

    #region ObterTodos
    
    [Fact]
    public async Task ObterTodosAsync_QuandoExistemCategorias_DeveRetornarListaDeCategorias()
    {
        var categorias = new List<Categoria>
        {
            CategoriaBuilder.Novo().Build(),
            CategoriaBuilder.Novo().Build()
        };
    
        _categoriaRepositoryMock.Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(categorias);
    
        var result = await _categoriaService.ObterTodosAsync();
    
        result.Should().NotBeNull();
        result.Should().HaveCount(categorias.Count);
        result.Should().AllBeOfType<CategoriaDto>();
        result.Should().BeEquivalentTo(categorias.Select(c => c.MapToDto()));
    }
    
    [Fact]
    public async Task ObterTodosAsync_QuandoNaoExistemCategorias_DeveRetornarListaVazia()
    {
        _categoriaRepositoryMock.Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(new List<Categoria>());
    
        var result = await _categoriaService.ObterTodosAsync();
    
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task ObterTodosAsync_QuandoErroNoBanco_DeveLancarException()
    {
        _categoriaRepositoryMock.Setup(r => r.ObterTodosAsync())
            .ThrowsAsync(new Exception("Falha no banco."));
    
        Func<Task> act = async () => await _categoriaService.ObterTodosAsync();
    
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Falha no banco.");
    }
    
    #endregion
}