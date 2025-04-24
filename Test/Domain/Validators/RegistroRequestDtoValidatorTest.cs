using Crosscutting.Validators.Auth.Register;
using Domain.Entities;
using Domain.Mappers;
using FluentAssertions;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class RegistroRequestDtoValidatorTest
{
    private readonly RegistroRequestDtoValidator _validator = new();

    [Fact]
    public void RegistroRequestDtoValidator_QuandoEmailValido_DeveRetornarSucesso()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail("example@example.com").Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));
        
        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void RegistroRequestDtoValidator_QuandoEmailVazio_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail(string.Empty).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == "Email é obrigatório.");
    }
    
    [Fact]
    public void RegistroRequestDtoValidator_QuandoEmailMaiorQueDuzentos_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail(new string('a', 201)).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == "Email deve ter no máximo 200 caracteres.");
    }
    
    [Fact]
    public void RegistroRequestDtoValidator_QuandoEmailInvalido_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail("emailinvalido").Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == "'emailinvalido' não é um email válido.");
    }

    [Fact]
    public void RegistroRequestDtoValidator_QuandoSenhaVazia_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComSenha(string.Empty).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Senha" &&
            e.ErrorMessage == "Senha é obrigatório.");
    }

    [Fact]
    public void RegistroRequestDtoValidator_QuandoSenhaMaiorQueCinquenta_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComSenha(new string('a', 201)).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Senha" &&
            e.ErrorMessage == "Senha deve ter no máximo 50 caracteres.");
    }
    
    [Fact]
    public void RegistroRequestDtoValidator_QuandoNomeCompletoMaiorQueCem_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComNomeCompleto(new string('a', 101)).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "NomeCompleto" &&
            e.ErrorMessage == "Nome Completo deve ter no máximo 100 caracteres.");
    }

    [Fact]
    public void RegistroRequestDtoValidator_QuandoNomeUsuarioMaiorQueCinquenta_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComNomeUsuario(new string('a', 51)).Build();

        var resultado = _validator.Validate(UsuarioMapper.MapToRegistroRequestDto(usuario));

        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "NomeUsuario" &&
            e.ErrorMessage == "Nome Usuario deve ter no máximo 50 caracteres.");
    }
}