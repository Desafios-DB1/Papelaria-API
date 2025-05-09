using Domain.Interfaces;
using Domain.Mappers;
using Domain.Querys;
using FluentAssertions;
using Moq;
using Test.Domain.Builders;

namespace Test.Domain.Querys.Categoria;

public class ObterCategoriaPorNomeQueryHandlerTest
{
    private readonly Mock<ICategoriaService> _categoriaServiceMock;
    private readonly ObterCategoriaPorNomeQueryHandler _handler;

    public ObterCategoriaPorNomeQueryHandlerTest()
    {
        _categoriaServiceMock = new Mock<ICategoriaService>();
        _handler = new ObterCategoriaPorNomeQueryHandler(_categoriaServiceMock.Object);
    }
    
    [Fact]
    public async Task Handle_QuandoQueryValida_DeveObterCategoria()
    {
        var categoria = CategoriaBuilder.Novo().ComNome("Categoria Teste").Build();
        var query = new ObterCategoriaPorNomeQuery(categoria.Nome);
        
        _categoriaServiceMock.Setup(x => x.ObterPorNomeAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoria.MapToResponseDto());

        var result = await _handler.Handle(query, CancellationToken.None);
        
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(categoria.MapToResponseDto());
    }

    [Fact]
    public async Task Handler_DeveChamarObterPorNomeAsyncApenasUmaVez()
    {
        var query = new ObterCategoriaPorNomeQuery("Teste");
        
        await _handler.Handle(query, CancellationToken.None);
        
        _categoriaServiceMock.Verify(x => x.ObterPorNomeAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_QuandoServicoLancarExcecao_DevePropagarExcecao()
    {
        var query = new ObterCategoriaPorNomeQuery("Teste");
        
        _categoriaServiceMock.Setup(x => x.ObterPorNomeAsync(query, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro ao obter categoria"));
        
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao obter categoria");
    }
}