using FluentValidation;
using PaperSquare.API.Feature.Auth.Dto;

namespace PaperSquare.Infrastructure.Features.Auth.Validators
{
    public class LoginInsertRequestValidator : AbstractValidator<LoginInsertRequest>
    {
        public LoginInsertRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(LoginInsertRequestOptions.PasswordLength)
                .WithMessage("Password must contain at least 8 characters!");            
        }
    }
}
