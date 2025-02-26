using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using BuildingBlock.Utilities;
using FluentValidation;
namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_ForgetPasswordCommand(string email) : ICommand<Result<bool>>;

public class Auth_ForgetPasswordCommandHandler : ICommandHandler<Auth_ForgetPasswordCommand, Result<bool>>
{
	private readonly DataContext _context;
	private readonly IEventBus _eventBus;

	public Auth_ForgetPasswordCommandHandler(
		DataContext context, IEventBus eventBus)
	{
		_eventBus = eventBus;
		_context = context;
	}

	public async Task<Result<bool>> Handle(Auth_ForgetPasswordCommand request, CancellationToken cancellationToken)
	{
		var user = await _context.Users.FirstOrDefaultAsync(s => s.Email == request.email);
		if(user == null)
		{
			throw new ApplicationException($"User not found with email : {request.email}");
		}
		if (user.StatusId == UserStatusConstant.BANNED)
		{
			throw new ApplicationException($"User account was banned");
		}
		if (user.IsEmailConfirmed == false)
		{
			throw new ApplicationException($"Your email is not confirmed");
		}

		await _eventBus.PublishAsync(new PushEmailEvent()
		{
			Email = user.Email,
			UserId = user.Id,
			Type = OTPConstant.ForgetPasswordType
		});

		return Result<bool>.Success(true);
	}
}
