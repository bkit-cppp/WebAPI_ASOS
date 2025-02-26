using Identity.API.Features.UserFeature.Dto;

namespace Identity.API.Features.UserFeature.Queries;

public record User_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<UserDto>>>;
public class User_GetAllQueryHandler : IQueryHandler<User_GetAllQuery, Result<IEnumerable<UserDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public User_GetAllQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<UserDto>>> Handle(User_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<UserDto> users = await _context.Users.OrderedListQuery(orderCol, orderDir)
												   .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
												   .ToListAsync();

		return Result<IEnumerable<UserDto>>.Success(users);
	}
}