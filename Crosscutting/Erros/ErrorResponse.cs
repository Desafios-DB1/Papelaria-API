namespace Crosscutting.Erros;

public abstract class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Error { get; set; } = string.Empty;
}