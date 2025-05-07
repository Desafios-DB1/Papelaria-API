namespace Crosscutting.Constantes;

public static class ErrorMessages
{
    public static string DtoNulo(string campo) => $"O DTO de {campo} não pode ser nulo.";
    public static string IdNulo(string campo) => $"O ID de {campo} não pode ser nulo.";
    public static string NomeNulo => "O campo nome não pode ser nulo.";
    public static string NaoExiste(string campo) => $"{campo} não existe.";
}