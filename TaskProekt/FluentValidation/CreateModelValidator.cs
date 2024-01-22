using FluentValidation;
using TaskProekt.ExensionFuntions;
using TaskProekt.ViewModels;

namespace TaskProekt.FluentValidation
{
    public class CreateModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 character");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is requier")
                .EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 character")
                .Must(CheckEmail.HaveCapitalLetter).WithMessage("Password must contain at least one capital letter");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword")
                .Equal(x => x.Password).WithMessage("Password do not match")
                .Must(CheckEmail.HaveCapitalLetter).WithMessage("Password must contain at least one capital letter");
        }
    }
}
