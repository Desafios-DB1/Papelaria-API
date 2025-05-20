using Crosscutting.Constantes;
using Domain.Commands.Categoria;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Validadores;

public class CriarCategoriaCommandValidator : AbstractValidator<CriarCategoriaCommand>
{
    public CriarCategoriaCommandValidator(ICategoriaRepository repository)
    {
        var categoriaRepository = repository;
        
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo)
            .Must(nome => !categoriaRepository.ExisteComNome(nome))
            .WithMessage(ValidationErrors.JaExiste(Entidades.Produto));

        RuleFor(x=>x.Descricao)
            .MaximumLength(Valores.Trezentos).WithMessage(ValidationErrors.TamanhoMaximo);
    }
}