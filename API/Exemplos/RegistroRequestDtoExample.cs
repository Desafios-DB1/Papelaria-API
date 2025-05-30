using Crosscutting.Dtos.Auth.Register;
using Swashbuckle.AspNetCore.Filters;

namespace API.Exemplos;

public class RegistroRequestDtoExample : IExamplesProvider<RegistroRequestDto>
{
    public RegistroRequestDto GetExamples()
    {
        return new RegistroRequestDto
        {
            NomeUsuario = "usuarioExemplo",
            Email = "exemplo@exemplo.com",
            NomeCompleto = "Usuário Exemplo",
            Senha = "SenhaForte123!",
        };
    }
}