using Domain.Commands.Categoria;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Test.Domain.Commands.Categoria;

public class CriarCategoriaCommandHandlerTest
{
    private readonly Mock<ICategoriaService> _categoriaServiceMock;
    private readonly CriarCategoriaCommandHandler _commandHandler;
    
    public CriarCategoriaCommandHandlerTest()
    {
        _categoriaServiceMock = new Mock<ICategoriaService>();
        _commandHandler = new CriarCategoriaCommandHandler(_categoriaServiceMock.Object);
    }
    
    [Fact]
    public async Task Handle_QuandoCommandValido_DeveCriarCategoria()
    {
        var command = new CriarCategoriaCommand
        {
            Nome = "Categoria Teste",
            Descricao = "Descrição Teste"
        };

        _categoriaServiceMock.Setup(x => x.CriarAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.NewGuid());
        
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_DeveChamarCriarAsyncApenasUmaVez()
    {
        var command = new CriarCategoriaCommand
        {
            Nome = "Categoria Teste",
            Descricao = "Descrição Teste"
        };

        await _commandHandler.Handle(command, CancellationToken.None);
        
        _categoriaServiceMock.Verify(x => x.CriarAsync(command, It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public async Task Handle_QuandoServicoLancarExcecao_DevePropagarExcecao()
    {
        var command = new CriarCategoriaCommand
        {
            Nome = "Categoria Teste",
            Descricao = "Descrição Teste"
        };

        _categoriaServiceMock.Setup(x => x.CriarAsync(command, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro ao criar categoria"));
        
        Func<Task> act = async () => await _commandHandler.Handle(command, CancellationToken.None);
        
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao criar categoria");
    }
}