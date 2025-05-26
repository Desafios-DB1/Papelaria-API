using Crosscutting.Constantes;
using Domain.Commands.LogProduto;
using FluentValidation;

namespace Domain.Validadores;

public class RegistrarLogProdutoCommandValidator : AbstractValidator<RegistrarLogProdutoCommand>
{
    public RegistrarLogProdutoCommandValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio);

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio);
        
        RuleFor(x => x.QuantidadeAnterior)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationErrors.ValorMinimo);

        RuleFor(x => x.QuantidadeAtual)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationErrors.ValorMinimo);
    }
}