using Crosscutting.Constantes;
using Domain.Commands.LogProduto;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validadores;

public class RegistrarLogProdutoCommandValidator : AbstractValidator<RegistrarLogProdutoCommand>
{
    public RegistrarLogProdutoCommandValidator(IQueryBase queryBase)
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MustAsync( async(categoriaId, _) => await queryBase.ExisteEntidadePorIdAsync<Produto>(categoriaId))
            .WithMessage(ValidationErrors.NaoExiste(Entidades.Produto));

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