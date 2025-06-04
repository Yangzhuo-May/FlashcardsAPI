using FlashcardsAPI.Dtos;
using FluentValidation;

namespace FlashcardsAPI.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator() 
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.")
            .WithName("User Name");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.")
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password cannot be empty.")
                .Equal(x => x.Password).WithMessage("Confirm password must match the password.")
                .WithName("Confirm Password");
        }
    }
}
