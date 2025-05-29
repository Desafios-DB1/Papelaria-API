using Crosscutting.Constantes;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Validadores;

public class AtualizarProdutoCommandValidator : AbstractValidator<AtualizarProdutoCommand>
{
    public AtualizarProdutoCommandValidator(IProdutoRepository produtoRepository, IQueryBase queryBase)
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo)
            .Must(nome => !produtoRepository.ExisteComNome(nome))
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
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MustAsync( async(categoriaId, _) => await queryBase.ExisteEntidadePorIdAsync<Categoria>(categoriaId))
            .WithMessage(ValidationErrors.NaoExiste(Entidades.Categoria));
    }
}