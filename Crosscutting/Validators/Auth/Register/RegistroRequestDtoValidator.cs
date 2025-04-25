using Crosscutting.Constantes;
using Crosscutting.Dtos.Auth.Register;
using FluentValidation;

namespace Crosscutting.Validators.Auth.Register;

public class RegistroRequestDtoValidator : AbstractValidator<RegistroRequestDto>
{
    public RegistroRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo)
            .EmailAddress().WithMessage(ValidationErrors.EmailInvalido);
        
        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Cinquenta).WithMessage(ValidationErrors.TamanhoMaximo);
        
        RuleFor(x=>x.NomeCompleto)
            .MaximumLength(Valores.Cem).WithMessage(ValidationErrors.TamanhoMaximo);

        RuleFor(x => x.NomeUsuario)
            .MaximumLength(Valores.Cinquenta).WithMessage(ValidationErrors.TamanhoMaximo);
    }
}