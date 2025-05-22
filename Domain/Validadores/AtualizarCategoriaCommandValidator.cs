using Crosscutting.Constantes;
using Domain.Commands.Categoria;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Validadores;

public class AtualizarCategoriaCommandValidator : AbstractValidator<AtualizarCategoriaCommand>
{
    public AtualizarCategoriaCommandValidator(ICategoriaRepository repository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio);
        
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo)
            .Must(nome => !repository.ExisteComNome(nome))
            .WithMessage(ValidationErrors.JaExiste(Entidades.Categoria));

        RuleFor(x=>x.Descricao)
            .MaximumLength(Valores.Trezentos).WithMessage(ValidationErrors.TamanhoMaximo);
    }
}