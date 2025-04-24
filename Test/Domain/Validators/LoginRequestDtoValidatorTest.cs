using Crosscutting.Validators.Auth.Login;
using Domain.Mappers;
using FluentAssertions;
using Test.Domain.Builders;

namespace Test.Domain.Validators;

public class LoginRequestDtoValidatorTest
{
    private readonly LoginRequestDtoValidator _validator = new();

    [Fact]
    public void LoginRequestDtoValidator_QuandoValido_DeveRetornarSucesso()
    {
        var usuario = UsuarioBuilder.Novo().Build();

        var resultado = _validator.Validate(UsuarioMapper.MapToLoginRequestDto(usuario));
        
        Assert.True(resultado.IsValid);
    }
    
    [Fact]
    public void LoginRequestDtoValidator_QuandoEmailVazio_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail(string.Empty).Build();

        var resultado = _validator.Validate(UsuarioMapper.MapToLoginRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == "Email é obrigatório.");
    }

    [Fact]
    public void LoginRequestDtoValidator_QuandoEmailMaiorQueDuzentos_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail(new string('a', 201)).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToLoginRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == "Email deve ter no máximo 200 caracteres.");
    }
    
    [Fact]
    public void LoginRequestDtoValidator_QuandoEmailInvalido_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComEmail("emailinvalido").Build();

        var resultado = _validator.Validate(UsuarioMapper.MapToLoginRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == "'emailinvalido' não é um email válido.");
    }
    
    [Fact]
    public void LoginRequestDtoValidator_QuandoSenhaVazia_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComSenha(string.Empty).Build();

        var resultado = _validator.Validate(UsuarioMapper.MapToLoginRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Senha" &&
            e.ErrorMessage == "Senha é obrigatório.");
    }

    [Fact]
    public void LoginRequestDtoValidator_QuandoSenhaMaiorQueCinquenta_DeveRetornarErro()
    {
        var usuario = UsuarioBuilder.Novo().ComSenha(new string('a', 51)).Build();
        
        var resultado = _validator.Validate(UsuarioMapper.MapToLoginRequestDto(usuario));
        
        resultado.Errors.Should().Contain(e =>
            e.PropertyName == "Senha" &&
            e.ErrorMessage == "Senha deve ter no máximo 50 caracteres.");
    }
}