using Crosscutting.Constantes;
using Crosscutting.Dtos.Produto;
using FluentValidation;

namespace Crosscutting.Validators.Produto;

public class ProdutoCreationRequestDtoValidator : AbstractValidator<ProdutoCreationRequestDto>
{
    public ProdutoCreationRequestDtoValidator()
    {
        RuleFor(x=>x.Nome)
        .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
        .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo);

        RuleFor(x=>x.Descricao)
        .MaximumLength(Valores.Trezentos).WithMessage(ValidationErrors.TamanhoMaximo);

        RuleFor(x=>x.QuantidadeMinima)
        .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo(0));

        RuleFor(x=>x.QuantidadeAtual)
        .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo(0));

        RuleFor(x=>x.PrecoCompra)
        .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo(0));

        RuleFor(x=>x.PrecoVenda)
        .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo(0));
    }
}
