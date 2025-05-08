namespace Crosscutting.Constantes;

public static class ErrorMessages
{
    public static string CampoNulo(string campo, string origem = "") =>
        string.IsNullOrEmpty(origem)
            ? $"O campo {campo} não pode ser nulo." 
            : $"O campo {campo} de {origem} não pode ser nulo.";
    public static string NaoExiste(string campo) => $"{campo} não existe.";
}