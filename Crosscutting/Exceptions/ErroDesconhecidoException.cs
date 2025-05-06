namespace Crosscutting.Exceptions;

public class ErroDesconhecidoException(string message, Exception exception) : Exception (message);