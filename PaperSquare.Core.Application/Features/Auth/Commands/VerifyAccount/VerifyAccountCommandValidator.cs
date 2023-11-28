using FluentValidation;

namespace PaperSquare.Core.Application.Features.Auth.Commands.VerifyAccount;

public sealed class VerifyAccountCommandValidator: AbstractValidator<VerifyAccountCommand>
{
    public VerifyAccountCommandValidator()
    {
        RuleFor(vac => vac.verificationCode).NotEmpty();
        RuleFor(vac => vac.userId).NotEmpty();
    }
}
