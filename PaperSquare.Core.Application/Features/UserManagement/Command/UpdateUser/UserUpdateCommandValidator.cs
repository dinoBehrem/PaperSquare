﻿using FluentValidation;
using PaperSquare.Core.Application.Features.UserManagement.Command.CreateUser;

namespace PaperSquare.Core.Application.Features.UserManagement.Command.UpdateUser;

internal sealed class UserUpdateCommandValidator: AbstractValidator<UpdateUserCommand>
{
    public UserUpdateCommandValidator()
    {
        RuleFor(user => user.firstName)
               .NotEmpty()
               .WithMessage(CreateUserValidationMessages.FieldIsRequired);

        RuleFor(user => user.lastName)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired);

        RuleFor(user => user.email)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired)
            .EmailAddress()
            .WithMessage(CreateUserValidationMessages.InvalidEmail);
    }
}
