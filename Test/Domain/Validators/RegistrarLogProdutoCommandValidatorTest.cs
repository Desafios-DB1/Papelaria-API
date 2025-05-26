using Crosscutting.Constantes;
using Domain.Commands.LogProduto;
using Domain.Validadores;
using FluentAssertions;

namespace Test.Domain.Validators;

public class RegistrarLogProdutoCommandValidatorTest
{
    private readonly RegistrarLogProdutoCommandValidator _validator = new();
    
    [Fact]
    public async Task Validate_QuandoProdutoIdVazio_DeveRetornarErro()
    {
        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = Guid.Empty,
            UsuarioId = Guid.NewGuid().ToString(),
            QuantidadeAnterior = 10,
            QuantidadeAtual = 5
        };

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "ProdutoId" &&
            e.ErrorMessage == "Produto Id é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoUsuarioIdVazio_DeveRetornarErro()
    {
        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = Guid.NewGuid(),
            UsuarioId = string.Empty,
            QuantidadeAnterior = 10,
            QuantidadeAtual = 5
        };

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "UsuarioId" &&
            e.ErrorMessage == "Usuario Id é obrigatório.");
    }
    
    [Fact]
    public async Task Validate_QuandoQuantidadeAnteriorNegativa_DeveRetornarErro()
    {
        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid().ToString(),
            QuantidadeAnterior = -1,
            QuantidadeAtual = 5
        };

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeAnterior" &&
            e.ErrorMessage == "Quantidade Anterior deve ser no mínimo 0.");
    }
    
    [Fact]
    public async Task Validate_QuandoQuantidadeAtualNegativa_DeveRetornarErro()
    {
        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid().ToString(),
            QuantidadeAnterior = 10,
            QuantidadeAtual = -1
        };

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "QuantidadeAtual" &&
            e.ErrorMessage == "Quantidade Atual deve ser no mínimo 0.");
    }
}