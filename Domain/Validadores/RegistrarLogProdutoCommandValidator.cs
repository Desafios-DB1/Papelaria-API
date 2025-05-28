using Crosscutting.Constantes;
using Domain.Commands.LogProduto;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Validadores;

public class RegistrarLogProdutoCommandValidator : AbstractValidator<RegistrarLogProdutoCommand>
{
    public RegistrarLogProdutoCommandValidator(IProdutoRepository produtoRepository)
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .Must(produtoRepository.ExisteComId).WithMessage(ValidationErrors.NaoExiste(Entidades.Produto));

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