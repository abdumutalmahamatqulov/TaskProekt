using FluentValidation;
using TaskProekt.Dto_s;
using TaskProekt.ExensionFuntions;

namespace TaskProekt.FluentValidation;

public class RegisterModelValidator : AbstractValidator<RegistorDto>
{
	public RegisterModelValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Name is required")
			.MinimumLength(3).WithMessage("Name must be at least 3 character");
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email  is requiest")
			.EmailAddress().WithMessage("Invalid email address");
		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Password is requiret")
			.MinimumLength(6).WithMessage("Password must be least 6 character")
			.Must(CheckEmail.HaveCapitalLetter).WithMessage("Password must contain at least one capital letter");
		RuleFor(x => x.ConfirmPassword)
			.NotEmpty().WithMessage("ConfirmPassword is rewuired")
			.Equal(x => x.Password).WithMessage("Password do not match")
			.Must(CheckEmail.HaveCapitalLetter).WithMessage("Password must contain at least one capital letter");
	}
}
