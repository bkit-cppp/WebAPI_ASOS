using BuildingBlock.Caching.Constants;
using BuildingBlock.Caching.Services;
using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using FluentValidation;
using Identity.API.Features.UserFeature.Dto;
using Identity.API.Interfaces;
using Identity.API.Models.AuthModel;

namespace Identity.API.Features.UserFeature.Commands;

public record User_UpdateProfileCommand(ProfileUpdateRequest RequestData) : ICommand<Result<UserDto>>;

public class UserUpdateProfileCommandValidator : AbstractValidator<User_UpdateProfileCommand>
{
	public UserUpdateProfileCommandValidator()
	{
		RuleFor(command => command.RequestData.Email).EmailRule();

		RuleFor(command => command.RequestData.Phone).PhoneRule();

		RuleFor(command => command.RequestData.Fullname).FullnameRule();

		RuleFor(command => command.RequestData.Address).AddressRule();
	}
}

public class User_UpdateProfileCommandHandler : ICommandHandler<User_UpdateProfileCommand, Result<UserDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;
	private readonly IUserService _userService;
	private readonly ICacheService _cacheService;

	public User_UpdateProfileCommandHandler(
		IMapper mapper,
		DataContext context,
		IEventBus eventBus,
		IUserService userService,
		ICacheService cacheService)
	{
		_context = context;
		_mapper = mapper;
		_eventBus = eventBus;
		_userService = userService;
		_cacheService = cacheService;
	}

	public async Task<Result<UserDto>> Handle(User_UpdateProfileCommand request, CancellationToken cancellationToken)
	{
		var user = await _userService.FindValidUser(request.RequestData.Id);

		var exist = await _context.Users
					.Where(u => u.Id != user.Id && u.Email == request.RequestData.Email)
					.FirstOrDefaultAsync();

		if(exist is not null)
		{
			throw new ApplicationException("Email already in uses");
		}

		user.Email = request.RequestData.Email;
		user.Phone = request.RequestData.Phone;
		user.Fullname = request.RequestData.Fullname;
		user.Address = request.RequestData.Address;
		user.Avatar = request.RequestData.Avatar ?? user.Avatar;

		_context.Users.Update(user);
		int rows = await _context.SaveChangesAsync();

		if (rows > 0)
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