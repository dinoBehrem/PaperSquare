using FluentValidation;
using PaperSquare.Core.Application.Features.UserManagement.Commands.CreateUser;

namespace PaperSquare.Core.Application.Features.UserManagement.Commands.DeleteUser;

internal sealed class DeleteUserCommandValidator: AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.id)
            .NotEmpty()
            .WithMessage(CreateUserValidationMessages.FieldIsRequired);
    }
}
