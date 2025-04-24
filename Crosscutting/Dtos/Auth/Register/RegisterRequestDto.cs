namespace Crosscutting.Dtos.Auth.Register;

public class RegisterRequestDto
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string NomeCompleto { get; set; }
    public string NomeUsuario { get; set; }
}
