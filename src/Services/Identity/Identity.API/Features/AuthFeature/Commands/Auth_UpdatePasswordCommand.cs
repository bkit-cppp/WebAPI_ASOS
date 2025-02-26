using FluentValidation;
using Identity.API.Models.AuthModel;

namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_UpdatePasswordCommand(UpdatePasswordRequest RequestData) : ICommand<Result<bool>>;

public class UpdatePasswordCommandValidator : AbstractValidator<Auth_UpdatePasswordCommand>
{
	public UpdatePasswordCommandValidator()
	{
		RuleFor(command => command.RequestData.CurrentPassword)
			.NotEmpty().WithMessage("Current password is required");

		RuleFor(command => command.RequestData.NewPassword)
			.NotEmpty().WithMessage("New password is required")
			.Matches(@"^[a-z0-9]*$").WithMessage("New password must only contain lowercase letters and numbers")
			.Matches(@"^\S*$").WithMessage("New password must not contain spaces")
			.Length(6, 15).WithMessage("New password must be between 6 and 15 characters long");
	}
}

public class Auth_UpdatePasswordCommandHandler : ICommandHandler<Auth_UpdatePasswordCommand, Result<bool>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Auth_UpdatePasswordCommandHandler(
		DataContext context,
		IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(Auth_UpdatePasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await _context.Users
			.FirstOrDefaultAsync(s =>
				s.Id == request.RequestData.Id &&
				s.Password == request.RequestData.CurrentPassword);

		if (user is null)
		{
			throw new ApplicationException("Current password is not correct");
		}

		user.Password = request.RequestData.NewPassword;
		user.ModifiedUser = request.RequestData.Id;
		user.ModifiedDate = DateTime.Now;

		_context.Users.Update(user);
		await _context.SaveChangesAsync();

		return Result<bool>.Success(true);
	}
}