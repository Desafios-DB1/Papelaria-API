using Crosscutting.Constantes;
using Domain.Commands.LogProduto;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.Validadores;
using FluentAssertions;
using Moq;

namespace Test.Domain.Validators;

public class RegistrarLogProdutoCommandValidatorTest
{
    private readonly Mock<IQueryBase> _queryBase = new();
    private readonly RegistrarLogProdutoCommandValidator _validator;

    public RegistrarLogProdutoCommandValidatorTest()
    {
        _validator = new RegistrarLogProdutoCommandValidator(_queryBase.Object);
    }
    
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
    
    [Fact]
    public async Task Validate_QuandoProdutoNaoExiste_DeveRetornarErro()
    {
        var produtoId = Guid.NewGuid();
        _queryBase.Setup(repo => repo.ExisteEntidadePorIdAsync<Produto>(produtoId))
            .ReturnsAsync(false);

        var command = new RegistrarLogProdutoCommand
        {
            ProdutoId = produtoId,
            UsuarioId = Guid.NewGuid().ToString(),
            QuantidadeAnterior = 10,
            QuantidadeAtual = 5
        };

        var resultado = await _validator.ValidateAsync(command);

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "ProdutoId" &&
            e.ErrorMessage == "Esse(a) Produto não existe.");
    }
}