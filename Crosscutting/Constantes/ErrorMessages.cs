namespace Crosscutting.Constantes;

public static class ErrorMessages
{
    public static string DtoNulo(string campo) => $"O DTO de {campo} não pode ser nulo.";
    public static string RequestNula => "A requisição não pode ser nula.";
}