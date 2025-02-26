using BuildingBlock.Caching.Constants;
using BuildingBlock.Caching.Services;
using FluentValidation;

namespace Identity.API.Features.UserFeature.Commands;

public record User_UpdateStatusCommand(UpdateStatusRequest RequestData) : ICommand<Result<bool>>;

public class UserUpdateStatusCommandValidator : AbstractValidator<User_UpdateStatusCommand>
{
	public UserUpdateStatusCommandValidator()
	{
		RuleFor(command => command.RequestData.Id).NotEmpty().WithMessage("ID not found");

		RuleFor(command => command.RequestData.Status).NotEmpty().WithMessage("Status not found");
	}
}

public class User_UpdateStatusCommandHandler : ICommandHandler<User_UpdateStatusCommand, Result<bool>>
{
	private readonly DataContext _context;
	private readonly ICacheService _cacheService;

	public User_UpdateStatusCommandHandler(
		DataContext context,
		ICacheService cacheService)
	{
		_context = context;
		_cacheService = cacheService;
	}

	public async Task<Result<bool>> Handle(User_UpdateStatusCommand request, CancellationToken cancellationToken)
	{
		var user = await _context.Users.FindAsync(request.RequestData.Id);

		if (user is null)
		{
			throw new ApplicationException($"ID User not found : {request.RequestData.Id}");
		}

		var status = await _context.Statuses.FindAsync(request.RequestData.Status);

		if (status is null)
		{
			throw new ApplicationException($"ID Status not found : {request.RequestData.Status}");
		}

		user.Status = status;
		user.StatusId = status.Id;
		user.ModifiedUser = request.RequestData.ModifiedUser;
		user.ModifiedDate = DateTime.Now;

		_context.Users.Update(user);
		int rows = await _context.SaveChangesAsync();
		if(rows > 0)
		{
			await _cacheService.RemoveCacheResponseAsync(IdentityCacheKey.User);
		}

		return Result<bool>.Success(true);
	}
}