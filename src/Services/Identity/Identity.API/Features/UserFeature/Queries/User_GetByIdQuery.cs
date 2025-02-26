using Identity.API.Features.UserFeature.Dto;

namespace Identity.API.Features.UserFeature.Queries;

public record User_GetByIdQuery(Guid Id) : IQuery<Result<UserDto>>;
public class User_GetByIdQueryHandler : IQueryHandler<User_GetByIdQuery, Result<UserDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public User_GetByIdQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<UserDto>> Handle(User_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var users = await _context.Users.Where(s => s.Id == request.Id)
								  .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
								  .FirstOrDefaultAsync();
		return Result<UserDto>.Success(users);
	}
}