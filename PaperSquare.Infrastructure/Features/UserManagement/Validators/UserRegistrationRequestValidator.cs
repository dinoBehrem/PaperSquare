using FluentValidation;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;

namespace PaperSquare.Infrastructure.Features.UserManagement.Validators
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(user => user.Firstname)
                .NotEmpty()
                .WithMessage(UserRegistrationRequestMessages.FieldIsRequired);

            RuleFor(user => user.Lastname)
                .NotEmpty()
                .WithMessage(UserRegistrationRequestMessages.FieldIsRequired);

            RuleFor(user => user.Username)
                .NotEmpty()
                .WithMessage(UserRegistrationRequestMessages.FieldIsRequired);

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(UserRegistrationRequestMessages.FieldIsRequired)
                .EmailAddress()
                .WithMessage(UserRegistrationRequestMessages.InvalidEmail);


            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage(UserRegistrationRequestMessages.FieldIsRequired)
                .MinimumLength(UserRegistrationRequestOptions.PasswordMinLength)
                .WithMessage(UserRegistrationRequestMessages.PasswordMinLength);

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty()
                .WithMessage(UserRegistrationRequestMessages.FieldIsRequired)
                .MinimumLength(UserRegistrationRequestOptions.PasswordMinLength)
                .WithMessage(UserRegistrationRequestMessages.PasswordMinLength)
                .Equal(user => user.Password)
                .WithMessage(UserRegistrationRequestMessages.ConfirmPasswordMatch);
        }
    }

    public class UserRegistrationRequestMessages
    {
        public const string FieldIsRequired = "Filed is required!";
        public const string InvalidEmail = "Invalid email address!";
        public const string PasswordMinLength = "Password must conatin at least 8 characters!";
        public const string ConfirmPasswordMatch = "Password and confirm password doesn`t match!";
    }
}
