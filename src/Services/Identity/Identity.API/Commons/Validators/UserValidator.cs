using FluentValidation;

namespace Identity.API.Commons.Validators;

public static class UserValidator
{
	public static IRuleBuilderOptions<T, string> EmailRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Email is required")
			.EmailAddress().WithMessage("Invalid email format");
	}

	public static IRuleBuilderOptions<T, string?> PasswordRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Password is required")
			.Matches(@"^[a-z0-9]*$").WithMessage("Password must only contain lowercase letters and numbers")
			.Matches(@"^\S*$").WithMessage("Password must not contain spaces")
			.Length(6, 15).WithMessage("Password must be between 6 and 15 characters long");
	}

	public static IRuleBuilderOptions<T, string> PhoneRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Phone is required")
			.Matches(@"^0\d{9}$").WithMessage("Phone number must have 10 digits and start with 0");
	}

	public static IRuleBuilderOptions<T, string> FullnameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Fullname is required");
	}

	public static IRuleBuilderOptions<T, string> AddressRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Address is required");
	}
}
