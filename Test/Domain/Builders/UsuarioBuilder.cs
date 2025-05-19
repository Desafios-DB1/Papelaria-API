using System.Security.Claims;
using Bogus;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Domain.Builders;

public class UsuarioBuilder
{
    private Faker<Usuario> _faker;

    public static UsuarioBuilder Novo()
    {
        return new UsuarioBuilder
        {
            _faker = new Faker<Usuario>()
                .RuleFor(u => u.NomeUsuario, f => f.Internet.UserName())
                .RuleFor(u=>u.NomeCompleto, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.DataCriacao, DateTime.Now)
                .RuleFor(u =>u.UserName, f => f.Internet.UserName())
                .RuleFor(u=>u.PasswordHash, f => f.Internet.Password())
        };
    }

    public UsuarioBuilder ComEmail(string email)
    {
        _faker.RuleFor(u => u.Email, email);
        return this;
    }

    public UsuarioBuilder ComSenha(string senha)
    {
        _faker.RuleFor(u => u.PasswordHash, senha);
        return this;
    }

    public UsuarioBuilder ComNomeCompleto(string nomeCompleto)
    {
        _faker.RuleFor(u=>u.NomeCompleto, nomeCompleto);
        return this;
    }

    public UsuarioBuilder ComNomeUsuario(string nomeUsuario)
    {
        _faker.RuleFor(u=>u.NomeUsuario, nomeUsuario);
        return this;
    }

    public static void SimularUsuarioAutenticado(ControllerBase controller, string userId = "usuario123")
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(ClaimTypes.NameIdentifier, userId)
        ], "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }
    
    public Usuario Build()
    {
        return _faker.Generate();
    }
}