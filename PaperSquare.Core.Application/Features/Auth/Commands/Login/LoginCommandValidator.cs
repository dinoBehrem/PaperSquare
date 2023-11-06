using FluentValidation;
using PaperSquare.API.Feature.Auth.Dto;

namespace PaperSquare.Core.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(lc => lc.username)
               .NotEmpty()
               .WithMessage("Username is required!");

            RuleFor(lc => lc.password)
                .NotEmpty()
                .MinimumLength(LoginInsertRequestOptions.PasswordLength)
                .WithMessage("Password must contain at least 8 characters!");
        }
    }
}
