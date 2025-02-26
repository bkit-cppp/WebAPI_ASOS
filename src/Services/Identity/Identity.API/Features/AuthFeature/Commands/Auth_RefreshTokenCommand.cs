using Identity.API.Features.AuthFeature.Dto;
using Identity.API.Interfaces;

namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_RefreshTokenCommand(string RefreshToken) : ICommand<Result<LoginDto>>;

public class Auth_RefreshTokenCommandHandler : ICommandHandler<Auth_RefreshTokenCommand, Result<LoginDto>>
{
	private readonly DataContext _context;
	private readonly ITokenService _tokenService;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	public Auth_RefreshTokenCommandHandler(
		DataContext context, 
		ITokenService tokenService,
		IUserService userService, 
		IMapper mapper)
	{
		_context = context;
		_tokenService = tokenService;
		_userService = userService;
		_mapper = mapper;
	}

	public async Task<Result<LoginDto>> Handle(Auth_RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var token = await _context.Tokens.FirstOrDefaultAsync(s => s.RefreshToken == request.RefreshToken);

		if (token is null || token.UserId is null)
		{
			throw new ApplicationException($"Token not found: {request.RefreshToken}");
		}

		if (DateTime.Now > token.ExpireAt)
		{
			throw new ApplicationException($"Refesh Token expired");
		}

		if (token.IsExpired == true)
		{
			throw new ApplicationException($"Refesh Token was used");
		}

		var user = await _userService.FindValidUser(token.UserId);

		if (user.RoleId is null || user.StatusId is null)
		{
			throw new ApplicationException("Account dont have permission to access");
		}

		token.IsExpired = true;
		token.ModifiedDate = DateTime.Now;
		token.ModifiedUser = token.UserId;

		var response = new LoginDto()
		{
			User = _mapper.Map<ProfileDto>(user),
			Token = await _tokenService.GenerateToken(
				user.Id, user.Email, user.Fullname,
				user.Avatar ?? AvatarConstant.Default,
				user.Address, user.Phone, user.RoleId,
				user.StatusId
			)
		};

		return Result<LoginDto>.Success(response);
	}
}
