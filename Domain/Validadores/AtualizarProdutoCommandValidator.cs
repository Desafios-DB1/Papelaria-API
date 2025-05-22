using Crosscutting.Constantes;
using Domain.Commands.Produto;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Validadores;

public class AtualizarProdutoCommandValidator : AbstractValidator<AtualizarProdutoCommand>
{
    public AtualizarProdutoCommandValidator(IProdutoRepository repository)
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo)
            .Must(nome => !repository.ExisteComNome(nome))
            .WithMessage(ValidationErrors.JaExiste(Entidades.Produto));

        RuleFor(x=>x.Descricao)
            .MaximumLength(Valores.Trezentos).WithMessage(ValidationErrors.TamanhoMaximo);

        RuleFor(x=>x.QuantidadeMinima)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo);

        RuleFor(x=>x.QuantidadeAtual)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo);
        
        RuleFor(x=>x.PrecoCompra)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo);

        RuleFor(x=>x.PrecoVenda)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationErrors.ValorMinimo);
        
        RuleFor(x => x.CategoriaId)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio);
    }
}