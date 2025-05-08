namespace Crosscutting.Constantes;

public static class ErrorMessages
{
    public static string CampoNulo(string campo, string objeto) =>
        $"O campo {campo} do objeto {objeto} não pode ser nulo.";
    public static string ObjetoNulo(string objeto) => $"O objeto {objeto} não pode ser nulo.";
    public static string NaoExiste(string campo) => $"{campo} não existe.";
}