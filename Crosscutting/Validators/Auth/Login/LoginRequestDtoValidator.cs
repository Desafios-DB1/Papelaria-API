using Crosscutting.Constantes;
using Crosscutting.Dtos.Auth.Login;
using FluentValidation;

namespace Crosscutting.Validators.Auth.Login;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Duzentos).WithMessage(ValidationErrors.TamanhoMaximo);
        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(ValidationErrors.CampoObrigatorio)
            .MaximumLength(Valores.Cinquenta).WithMessage(ValidationErrors.TamanhoMaximo);
    }
}