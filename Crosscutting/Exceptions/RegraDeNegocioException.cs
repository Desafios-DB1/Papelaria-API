namespace Crosscutting.Exceptions;

public class RegraDeNegocioException : Exception
{
    public RegraDeNegocioException(string message) : base(message)
    {
    }

    public RegraDeNegocioException(IEnumerable<string> messages) : base(string.Join(", ", messages))
    {
    }
}