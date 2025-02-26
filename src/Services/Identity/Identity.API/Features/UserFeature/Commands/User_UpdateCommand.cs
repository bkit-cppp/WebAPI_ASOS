using BuildingBlock.Caching.Constants;
using BuildingBlock.Caching.Services;
using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using FluentValidation;
using Identity.API.Features.UserFeature.Dto;
using Identity.API.Models.UserModel;

namespace Identity.API.Features.UserFeature.Commands;

public record User_UpdateCommand(UserAddOrUpdateRequest RequestData) : ICommand<Result<UserDto>>;

public class UserUpdateCommandValidator : AbstractValidator<User_UpdateCommand>
{
	public UserUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Email).EmailRule();

		RuleFor(command => command.RequestData.Phone).PhoneRule();

		RuleFor(command => command.RequestData.Fullname).FullnameRule();

		RuleFor(command => command.RequestData.Address).AddressRule();
	}
}

public class User_UpdateCommandHandler : ICommandHandler<User_UpdateCommand, Result<UserDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;
	private readonly ICacheService _cacheService;

	public User_UpdateCommandHandler(
		DataContext context,
		IMapper mapper, 
		IEventBus eventBus,
		ICacheService cacheService)
	{
		_context = context;
		_mapper = mapper;
		_eventBus = eventBus;
		_cacheService = cacheService;
	}

	public async Task<Result<UserDto>> Handle(User_UpdateCommand request, CancellationToken cancellationToken)
	{
		var user = await _context.Users
								 .Include(s => s.Status)
								 .Include(s => s.Role)
								 .FirstOrDefaultAsync(s => s.Id == request.RequestData.Id);

		if (user is null)
		{
			throw new ApplicationException("Email or password are not correct");
		}

		if (user.StatusId == UserStatusConstant.BANNED)
		{
			throw new ApplicationException("Account was banned");
		}

		user.Email = request.RequestData.Email;
		user.Phone = request.RequestData.Phone;
		user.Fullname = request.RequestData.Fullname;
		user.Address = request.RequestData.Address;

		if (!string.IsNullOrEmpty(request.RequestData.StatusId))
		{
			var status = await _context.Statuses.FindAsync(request.RequestData.StatusId);
			if (status != null)
			{
				user.StatusId = status.Id;
				user.Status = status;
			}
		}

		if (!string.IsNullOrEmpty(request.RequestData.RoleId))
		{
			var role = await _context.Roles.FindAsync(request.RequestData.RoleId);
			if (role != null)
			{
				user.RoleId = role.Id;
				user.Role = role;
			}
		}

		if (request.RequestData.IsEmailConfirmed != null)
		{
			user.IsEmailConfirmed = request.RequestData.IsEmailConfirmed.Value;
		}

		_context.Users.Update(user);
		int rows = await _context.SaveChangesAsync();

		if(rows > 0)
		{
			await _eventBus.PublishAsync(new UserUpdatedEvent
			{
				Id = user.Id,
				Email = user.Email,
				Fullname = user.Fullname,
				Avatar = user.Avatar ?? AvatarConstant.Default
			});
			await _cacheService.RemoveCacheResponseAsync(IdentityCacheKey.User);
		}

		return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
	}
}