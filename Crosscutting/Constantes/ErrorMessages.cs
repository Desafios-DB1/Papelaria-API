namespace Crosscutting.Constantes;

public static class ErrorMessages
{
    public static string CampoNulo(string campo, string objeto = "") =>
        string.IsNullOrEmpty(objeto)
            ? $"O campo {campo} não pode ser nulo."
            : $"O campo {campo} do objeto {objeto} não pode ser nulo.";
    public static string ObjetoNulo(string objeto) => $"O objeto {objeto} não pode ser nulo.";
    public static string NaoExiste(string item) => $"{item} não existe.";
}