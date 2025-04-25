using Crosscutting.Validators.Categoria;
using Domain.Mappers;
using FluentAssertions;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class CategoriaCreationRequestValidatorTest
{
    private readonly CategoriaCreationRequestDtoValidator _validator = new();
    
    [Fact]
    public void CategoriaCreationRequestDtoValidator_QuandoCategoriaValida_DeveRetornarSucesso()
    {
        var categoria = CategoriaBuilder.Novo().Build();

        var resultado = _validator.Validate(CategoriaMapper.MapToCreationDto(categoria));

        resultado.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void CategoriaCreationRequestDtoValidator_QuandoNomeVazio_DeveRetornarErro()
    {
        var categoria = CategoriaBuilder.Novo().ComNome(string.Empty).Build();

        var resultado = _validator.Validate(CategoriaMapper.MapToCreationDto(categoria));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome é obrigatório.");
    }
    
    [Fact]
    public void CategoriaCreationRequestDtoValidator_QuandoNomeMaiorQueDuzentos_DeveRetornarErro()
    {
        var categoria = CategoriaBuilder.Novo().ComNome(new string('a', 201)).Build();

        var resultado = _validator.Validate(CategoriaMapper.MapToCreationDto(categoria));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == "Nome deve ter no máximo 200 caracteres.");
    }

    [Fact]
    public void CategoriaCreationRequestDtoValidator_QuandoDescricaoMaiorQueTrezentos_DeveRetornarErro()
    {
        var categoria = CategoriaBuilder.Novo().ComDescricao(new string('a', 301)).Build();
        
        var resultado = _validator.Validate(CategoriaMapper.MapToCreationDto(categoria));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Descricao" &&
            e.ErrorMessage == "Descricao deve ter no máximo 300 caracteres.");
    }
}