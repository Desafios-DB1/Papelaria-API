namespace Crosscutting.Erros;

public class ErrorResponse(string message)
{
    public string Message { get; set; } = message;
}