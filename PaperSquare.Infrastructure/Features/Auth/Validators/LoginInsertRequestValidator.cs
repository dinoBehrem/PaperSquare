using FluentValidation;
using PaperSquare.API.Feature.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Features.Auth.Validators
{
    public class LoginInsertRequestValidator : AbstractValidator<LoginInsertRequest>
    {
        public LoginInsertRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(LoginInsertRequestOptions.PasswordLength)
                .WithMessage("Password must contain at least 8 characters!");            
        }
    }
}
