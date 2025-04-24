using Bogus;
using Domain.Entities;

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
    
    public Usuario Build()
    {
        return _faker.Generate();
    }
}