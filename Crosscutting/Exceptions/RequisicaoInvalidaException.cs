using Crosscutting.Constantes;
using FluentValidation.Results;

namespace Crosscutting.Exceptions;

public class RequisicaoInvalidaException(string message) : Exception(message)
{
    public RequisicaoInvalidaException(List<ValidationFailure> erros) : this(ValidationErrors.ErrosDeValidacao) { }
}