using Crosscutting.Constantes;
using Domain.Commands.Categoria;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Validadores;

public class RemoverCategoriaCommandValidator : AbstractValidator<RemoverCategoriaCommand>
{
    public RemoverCategoriaCommandValidator(IProdutoRepository produtoRepository)
    {
        RuleFor(x => x.CategoriaId)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .Must(categoriaId => !produtoRepository.ExisteComCategoriaId(categoriaId))
            .WithMessage(ValidationErrors.CategoriaPossuiProdutos);
    }
}   