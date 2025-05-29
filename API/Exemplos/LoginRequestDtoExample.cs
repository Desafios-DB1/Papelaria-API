using Crosscutting.Dtos.Auth.Login;
using Swashbuckle.AspNetCore.Filters;

namespace API.Exemplos;

public class LoginRequestDtoExample : IExamplesProvider<LoginRequestDto>
{
    public LoginRequestDto GetExamples()
    {
        return new LoginRequestDto
        {
            Email = "example@example.com",
            Senha = "SenhaForte123!"
        };
    }
}