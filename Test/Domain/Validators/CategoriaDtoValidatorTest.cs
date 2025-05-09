using Crosscutting.Validators.Categoria;
using Domain.Mappers;
using FluentAssertions;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class CategoriaDtoValidatorTest
{
    private readonly CategoriaDtoValidator _validator = new();
    
    [Fact]
    public void CategoriaDtoValidator_QuandoCategoriaValida_DeveRetornarSucesso()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        var resultado = _validator.Validate(CategoriaMapper.MapToDto(categoria));

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CategoriaDtoValidator_QuandoNomeVazio_DeveRetornarErro()
    {
        var categoria = CategoriaBuilder.Novo().ComNome(string.Empty).Build();

        var resultado = _validator.Validate(CategoriaMapper.MapToDto(categoria));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public void CategoriaDtoValidator_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var categoria = CategoriaBuilder.Novo().ComNome(new string('a', 201)).Build();

        var resultado = _validator.Validate(CategoriaMapper.MapToDto(categoria));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }

    [Fact]
    public void CategoriaDtoValidator_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var categoria = CategoriaBuilder.Novo().ComDescricao(new string('a', 301)).Build();
        
        var resultado = _validator.Validate(CategoriaMapper.MapToDto(categoria));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
}