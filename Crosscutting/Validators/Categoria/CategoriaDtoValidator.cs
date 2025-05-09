using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using FluentValidation;

namespace Crosscutting.Validators.Categoria;

public class CategoriaDtoValidator : AbstractValidator<CategoriaDto>
{
    public CategoriaDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo);

        RuleFor(x => x.Descricao)
            .MaximumLength(Valores.Trezentos).WithMessage(ValidationErrors.TamanhoMaximo);
        
        RuleFor(x => x.Ativo)
            .NotNull().WithMessage(ValidationErrors.CampoObrigatorio);
    }
}